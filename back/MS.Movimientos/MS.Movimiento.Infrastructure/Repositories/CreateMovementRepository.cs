using MS.Movimiento.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using MS.Movimiento.Domain.Entities;


namespace MS.Movimiento.Infrastructure.Repositories
{
    public class CreateMovementRepository : ICreateMovementRepository
    {
        private readonly string _connectionString;

        public CreateMovementRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
              ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        public async Task CreateMovementAsync(MovementCab input)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_CreateMovimiento", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agregar parámetros del procedimiento almacenado
                command.Parameters.AddWithValue("@IdTipoMovimiento", input.IdTipoMovimiento?.idTipoMovimiento ?? 0);
                command.Parameters.AddWithValue("@IdDocumentoOrigen", input.IdDocumentoOrigen?.idDocumentoOrigen ?? 0);

                var table = new DataTable();
                table.Columns.Add("IdProducto", typeof(int));
                table.Columns.Add("Cantidad", typeof(int));


                foreach (var det in input.MovementDet ?? new List<MovementDet>())
                {
                    table.Rows.Add(
                        det.IdProducto?.idProducto ?? 0,
                        det.Cantidad?.cantidad ?? 0
                        
                    );
                }

                var param = command.Parameters.AddWithValue("@MovementDetList", table);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "MovimientoDetTableType";

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException($"Error al registrar el movimiento en la base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error inesperado al registrar el movimiento: {ex.Message}", ex);
            }
        }


    }
}
