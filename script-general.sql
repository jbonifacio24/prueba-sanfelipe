
/*SCRIPT GENERAL*/

USE master
GO
/************************************/
/**** CREA BASE DATOS ProductoDB ****/
/************************************/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ProductoDB')
BEGIN
    CREATE DATABASE ProductoDB
    PRINT 'Base de datos ProductoDB creada exitosamente.'
END
ELSE
BEGIN
    PRINT 'La base de datos ProductoDB ya existe.'
END
GO
/************************************/
/***** CREA BASE DATOS VentaDB ******/
/************************************/
USE master
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'VentaDB')
BEGIN
    CREATE DATABASE VentaDB
    PRINT 'Base de datos VentaDB creada exitosamente.'
END
ELSE
BEGIN
    PRINT 'La base de datos VentaDB ya existe.'
END
GO
/*************************************/
/***** CREA BASE DATOS CompraDB ******/
/*************************************/
USE master
GO
-- CREA LA BASE DE DATOS SI NO EXISTE
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CompraDB')
BEGIN
    CREATE DATABASE CompraDB
    PRINT 'Base de datos CompraDB creada exitosamente.'
END
ELSE
BEGIN
    PRINT 'La base de datos CompraDB ya existe.'
END
GO

/*****************************************/
/***** CREA BASE DATOS MovimientoDB ******/
/*****************************************/
USE master
GO
-- CREA LA BASE DE DATOS SI NO EXISTE
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MovimientoDB')
BEGIN
    CREATE DATABASE MovimientoDB
    PRINT 'Base de datos MovimientoDB creada exitosamente.'
END
ELSE
BEGIN
    PRINT 'La base de datos MovimientoDB ya existe.'
END
GO


/*****************************************/
/******* SCRIPT PARA PRODUCTOS ***********/
/*****************************************/
USE ProductoDB
GO
--CREA TABLA PRODUCTOS
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Productos]') AND type in (N'U'))
BEGIN
		CREATE TABLE dbo.Productos(
		IdProducto int,
		NombreProducto varchar(200) ,
		NroLote int,
		FecRegistro Datetime,
		Costo numeric(20,10),
		PrecioVenta numeric(20,10)
		CONSTRAINT PK_Producto PRIMARY KEY CLUSTERED
		(
		IdProducto ASc
		)  )
		ON [PRIMARY]
		
END
GO

CREATE OR ALTER PROCEDURE SP_CreateProduct 

@IdProducto int,
@NombreProducto NVARCHAR(255),
@NroLote INT,
@FecRegistro DATETIME,
@Costo numeric(20,10),
@PrecioVenta numeric(20,10)
AS

BEGIN
	SET NOCOUNT ON;

	DECLARE @ID INT = isnull((SELECT MAX(IdProducto) FROM Productos ) + 1,1)

	INSERT INTO Productos (IdProducto, NombreProducto, NroLote, FecRegistro, Costo, PrecioVenta)
	VALUES (@ID, @NombreProducto, @NroLote, @FecRegistro, @Costo, @PrecioVenta)

END
GO
CREATE OR ALTER PROCEDURE SP_GetAllProducts

AS

BEGIN
	SET NOCOUNT ON;
	SELECT  
       IdProducto,
	   NombreProducto,
	   NroLote,
	   FecRegistro,
	   Costo,
	   PrecioVenta
    FROM dbo.Productos
	Order By NombreProducto Asc



END
GO
/*****************************************/
/******** SCRIPT PARA VENTAS *************/
/*****************************************/
GO
USE VentaDB
GO
--CREA TABLA VENTA
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[VentaDB].[dbo].[VentaCab]') AND type in (N'U'))
BEGIN
		CREATE TABLE VentaDB.dbo.VentaCab(
		IdVentaCab int,
		FecRegistro Datetime,
		SubTotal numeric(20,10),
		Igv numeric(20,10),
		Total numeric(20,10)

		CONSTRAINT PK_Venta PRIMARY KEY CLUSTERED
		(
		IdVentaCab Desc
		)  )
		ON [PRIMARY]
		
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[VentaDB].[dbo].[VentaDet]') AND type in (N'U'))
BEGIN
		CREATE TABLE VentaDB.dbo.VentaDet(
		IdVentaDet int,
		IdVentaCab int,
		IdProducto Datetime,
		Cantidad Int,
		Precio decimal,
		SubTotal decimal,
		Igv decimal,
		Total decimal

		CONSTRAINT PK_VentaDet PRIMARY KEY CLUSTERED
		(
		IdVentaDet Desc
		)  )
		ON [PRIMARY]
		
END

/******************************************/
/******** SCRIPT PARA COMPRAS *************/
/******************************************/
GO
USE CompraDB
GO

--CREA TABLA COMPRA
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompraDB].[dbo].[CompraCab]') AND type in (N'U'))
BEGIN
		CREATE TABLE dbo.CompraCab(
		IdCompraCab int IDENTITY(1,1),
		FecRegistro Datetime default(getdate()),
		SubTotal numeric(20,10),
		Igv numeric(20,10),
		Total numeric(20,10)

		CONSTRAINT PK_Venta PRIMARY KEY CLUSTERED
		(
		IdCompraCab Desc
		)  )
		ON [PRIMARY]
		
END  

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CompraDB].[dbo].[CompraDet]') AND type in (N'U'))
BEGIN
		CREATE TABLE dbo.CompraDet(
		IdCompraDet int identity(1,1),
		IdCompraCab int,
		IdProducto INT,
		Cantidad Int,
		Precio numeric(20,10),
		SubTotal numeric(20,10),
		Igv numeric(20,10),
		Total numeric(20,10)

		CONSTRAINT PK_VentaDet PRIMARY KEY CLUSTERED
		(
		IdCompraCab,IdCompraDet Desc
		)  )
		ON [PRIMARY]
		
END
GO
DROP PROCEDURE IF EXISTS SP_CreateCompra;
GO
GO
--CREA TYPE PARA LISTAS
IF TYPE_ID('dbo.CompraDetTableType') IS NOT NULL
    DROP TYPE dbo.CompraDetTableType;
GO
CREATE TYPE dbo.CompraDetTableType AS TABLE
(
    IdProductoDet INT,
    CantidadDet DECIMAL(18,2),
    PrecioDet DECIMAL(18,2),
    SubTotalDet DECIMAL(18,2),
    IgvDet DECIMAL(18,2),
    TotalDet DECIMAL(18,2)
);
GO
--CREA PROCEDIMIENTO PARA REGISTRAR
CREATE PROCEDURE SP_CreateCompra
(
    @SubTotal DECIMAL(18,2),
    @Igv DECIMAL(18,2),
    @Total DECIMAL(18,2),
	@IdCompraCabOutput INT OUTPUT,
    @OrderDetList CompraDetTableType READONLY
	
)
AS
BEGIN

    SET NOCOUNT ON;
    BEGIN TRY
    BEGIN TRANSACTION;

        -- Insert Cabecera
        INSERT INTO dbo.CompraCab
        (
            SubTotal,
            Igv,
            Total
        )
        VALUES
        (
            @SubTotal,
            @Igv,
            @Total
        );

        DECLARE @IdCompraCab INT = SCOPE_IDENTITY();

        -- Insert Detalle
        INSERT INTO dbo.CompraDet
        (
            IdCompraCab,
            IdProducto,
            Cantidad,
            Precio,
            SubTotal,
            Igv,
            Total
        )
        SELECT
            @IdCompraCab,
            IdProductoDet,
            CantidadDet,
            PrecioDet,
            SubTotalDet,
            IgvDet,
            TotalDet
        FROM @OrderDetList;

		SET @IdCompraCabOutput = @IdCompraCab;

        COMMIT TRANSACTION;

     

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- devolver error real
        THROW;

    END CATCH

END
GO
CREATE OR ALTER PROCEDURE SP_GetAllCompras
AS
BEGIN

	Select 
		IdCompraCab,
		FecRegistro,
		SubTotal,
		Igv,
		Total
	From CompraCab

END
GO
CREATE OR ALTER PROCEDURE SP_GetOrderDetailsByCabId

	@IdCompraCab INT =1

AS
BEGIN

	Select 
		IdCompraDet,
		IdCompraCab,
		IdProducto,
		Cantidad,
		Precio,
		SubTotal,
		Igv,
		Total
	From CompraDet
	wHERE IdCompraCab = @IdCompraCab



END
GO
/**********************************************/
/******** SCRIPT PARA MOVIMIENTOS *************/
/**********************************************/
USE MovimientoDB
GO
--CREA TABLA MOVIMIENTOS
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MovimientoDB].[dbo].[MovimientoCab]') AND type in (N'U'))
BEGIN
		CREATE TABLE dbo.MovimientoCab(
		IdMovimientoCab int IDENTITY(1,1),
		FecRegistro Datetime default(getdate()),
		IdTipoMovimiento int,
		IdDocumentoOrigen int

		CONSTRAINT PK_Venta PRIMARY KEY CLUSTERED
		(
		IdMovimientoCab Desc
		)  )
		ON [PRIMARY]
		
END  

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MovimientoDB].[dbo].[MovimientoDet]') AND type in (N'U'))
BEGIN
		CREATE TABLE dbo.MovimientoDet(
		IdMovimientoDet int identity(1,1),
		IdMovimientoCab int,
		IdProducto INT,
		Cantidad Int
		CONSTRAINT PK_MovimientoDet PRIMARY KEY CLUSTERED
		(
		IdMovimientoCab,IdMovimientoDet Desc
		)  )
		ON [PRIMARY]
		
END
GO

DROP PROCEDURE IF EXISTS SP_CreateMovimiento;
GO
--CREA TYPE PARA LISTAS
IF TYPE_ID('dbo.MovimientoDetTableType') IS NOT NULL
    DROP TYPE dbo.MovimientoDetTableType;
GO
CREATE TYPE dbo.MovimientoDetTableType AS TABLE
(
    IdProducto INT,
    Cantidad INT
);
GO
CREATE PROCEDURE SP_CreateMovimiento
	@IdTipoMovimiento INT,
    @IdDocumentoOrigen INT,
	@MovementDetList MovimientoDetTableType READONLY
AS

BEGIN

	 SET NOCOUNT ON;
    BEGIN TRY
    BEGIN TRANSACTION;

        -- Insert Cabecera
        INSERT INTO dbo.MovimientoCab
        (
            IdTipoMovimiento ,
			IdDocumentoOrigen 
        )
        VALUES
        (
            @IdTipoMovimiento,
            @IdDocumentoOrigen
        );

        DECLARE @IdMovimientoCab INT = SCOPE_IDENTITY();

        -- Insert Detalle
        INSERT INTO dbo.MovimientoDet
        (
            IdMovimientoCab,
            IdProducto,
            Cantidad
        )
        SELECT
            @IdMovimientoCab,
            IdProducto,
            Cantidad
        FROM @MovementDetList;

        COMMIT TRANSACTION;

     

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- devolver error real
        THROW;

    END CATCH

END