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
using MS.Compra.Domain.ValueObjects;
using System.Reflection.Metadata.Ecma335;

namespace MS.Compra.Infrastructure.Repositories
{
    public class GetAllOrdersRepository : IGetAllOrdersRepository
    {
        private readonly string _connectionString;

        public GetAllOrdersRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
              ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<List<OrderCab>> GetAllOrdersAsync()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_GetAllCompras", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                await connection.OpenAsync();


                var output = new List<OrderCab>();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {

                    var objeto = new OrderCab
                    {
                        IdCompraCab = reader.GetInt32(reader.GetOrdinal("IdCompraCab")),
                        FecRegistro = reader.GetDateTime(reader.GetOrdinal("FecRegistro")),
                        SubTotalCab = reader.IsDBNull(reader.GetOrdinal("SubTotal")) ? null : new SubTotalCab(reader.GetDecimal(reader.GetOrdinal("SubTotal"))),
                        IgvCab = reader.IsDBNull(reader.GetOrdinal("Igv")) ? null : new IgvCab(reader.GetDecimal(reader.GetOrdinal("Igv"))),
                        TotalCab = reader.IsDBNull(reader.GetOrdinal("Total")) ? null : new TotalCab(reader.GetDecimal(reader.GetOrdinal("Total"))),

                    };
                    output.Add(objeto);
                }
                reader.Close(); 

                foreach (var item in output)
                {
                    var commandDet = new SqlCommand("SP_GetOrderDetailsByCabId", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    commandDet.Parameters.AddWithValue("@IdCompraCab", item.IdCompraCab);
                    using var readerDet = await commandDet.ExecuteReaderAsync();
                    var detalles = new List<OrderDet>();
                    while (await readerDet.ReadAsync())
                    {
                        var detalle = new OrderDet
                        {
                            IdCompraDet = readerDet.GetInt32(readerDet.GetOrdinal("IdCompraDet")),
                            IdCompraCab = readerDet.GetInt32(readerDet.GetOrdinal("IdCompraCab")),
                            IdProductoDet = new IdProductoDet(readerDet.GetInt32(readerDet.GetOrdinal("IdProducto"))),
                            CantidadDet = new CantidadDet(readerDet.GetInt32(readerDet.GetOrdinal("Cantidad"))),
                            PrecioDet = new PrecioDet(readerDet.GetDecimal(readerDet.GetOrdinal("Precio"))),
                            SubTotalDet = new SubTotalDet(readerDet.GetDecimal(readerDet.GetOrdinal("SubTotal"))),
                            IgvDet = readerDet.IsDBNull(readerDet.GetOrdinal("Igv")) ? null : new IgvDet(readerDet.GetDecimal(readerDet.GetOrdinal("Igv"))),
                            TotalDet = readerDet.IsDBNull(readerDet.GetOrdinal("Total")) ? null : new TotalDet(readerDet.GetDecimal(readerDet.GetOrdinal("Total")))
                        };
                        detalles.Add(detalle);
                    }
                    item.OrderDet = detalles;

                }


                return output;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException($"Error al listar las compras en la base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error inesperado al listar las compras: {ex.Message}", ex);
            }
        }
    }
}
