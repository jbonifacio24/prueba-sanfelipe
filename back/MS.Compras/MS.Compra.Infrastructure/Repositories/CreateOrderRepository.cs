using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Compra.Domain.Entities;
using MS.Compra.Domain.Interfaces;
using MS.Compra.Infrastructure.Mappers;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MS.Compra.Infrastructure.Repositories
{
    public class CreateOrderRepository : ICreateOrderRepository
    {
        private readonly string _connectionString;

        public CreateOrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
              ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<int> CreateOrderAsync(OrderCab input)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_CreateCompra", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agregar parámetros del procedimiento almacenado
                command.Parameters.AddWithValue("@SubTotal", input.SubTotalCab?.subTotalCab ?? 0);
                command.Parameters.AddWithValue("@Igv", input.IgvCab?.igv ?? 0);
                command.Parameters.AddWithValue("@Total", input.TotalCab?.totalCab ?? 0);

                var table = new DataTable();
                table.Columns.Add("IdProductoDet", typeof(int));
                table.Columns.Add("CantidadDet", typeof(decimal));
                table.Columns.Add("PrecioDet", typeof(decimal));
                table.Columns.Add("SubTotalDet", typeof(decimal));
                table.Columns.Add("IgvDet", typeof(decimal));
                table.Columns.Add("TotalDet", typeof(decimal));

                foreach (var det in input.OrderDet ?? new List<OrderDet>())
                {
                    table.Rows.Add(
                        det.IdProductoDet?.idProducto ?? 0,
                        det.CantidadDet?.cantidad ?? 0,
                        det.PrecioDet?.precioDet ?? 0,
                        det.SubTotalDet?.subTotalDet ?? 0,
                        det.IgvDet?.igvdet ?? 0,
                        det.TotalDet?.totalDet ?? 0
                    );
                }

                var param = command.Parameters.AddWithValue("@OrderDetList", table);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "CompraDetTableType";

                
                var outputIdParam = new SqlParameter("@IdCompraCabOutput", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                
                int idCompraCab = (int)outputIdParam.Value;
                return idCompraCab;
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
