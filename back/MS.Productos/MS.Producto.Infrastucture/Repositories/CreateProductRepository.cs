using MS.Producto.Domain.Entities;
using MS.Producto.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MS.Producto.Infrastucture.Repositories
{
    public class CreateProductRepository : ICreateProductRepository
    {
        private readonly string _connectionString;
        public CreateProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        public async Task CreateProductAsync(Product input)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_CreateProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agregar parámetros del procedimiento almacenado
                command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = input.IdProducto;
                command.Parameters.Add("@NombreProducto", SqlDbType.NVarChar, 255).Value = input.NombreProducto?.name ?? (object)DBNull.Value;
                command.Parameters.Add("@NroLote", SqlDbType.Int).Value = input.NroLote?.nroLote ?? (object)DBNull.Value;
                command.Parameters.Add("@FecRegistro", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@Costo", SqlDbType.Decimal).Value = input.Costo?.amount ?? (object)DBNull.Value;
                command.Parameters.Add("@PrecioVenta", SqlDbType.Decimal).Value = input.PrecioVenta?.amount ?? (object)DBNull.Value;

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException($"Error al registrar el producto en la base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error inesperado al registrar el producto: {ex.Message}", ex);
            }
        }
    }
}
