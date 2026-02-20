using Microsoft.Data.SqlClient;
using MS.Producto.Domain.Entities;
using MS.Producto.Domain.Interfaces;
using MS.Producto.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Infrastucture.Repositories
{
    public class GetAllProductsRepository : IGetAllProductsRepository
    {
        private readonly string _connectionString;
        public GetAllProductsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_GetAllProducts", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                await connection.OpenAsync();

                var products = new List<Product>();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var product = new Product
                    {
                        IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                        NombreProducto = reader.IsDBNull(reader.GetOrdinal("NombreProducto")) ? null : new NombreProducto(reader.GetString(reader.GetOrdinal("NombreProducto"))),
                        NroLote = reader.IsDBNull(reader.GetOrdinal("NroLote")) ? null : new NroLote(reader.GetInt32(reader.GetOrdinal("NroLote"))),
                        FecRegistro = reader.GetDateTime(reader.GetOrdinal("FecRegistro")),
                        Costo = reader.IsDBNull(reader.GetOrdinal("Costo")) ? null : new Costo(reader.GetDecimal(reader.GetOrdinal("Costo"))),
                        PrecioVenta = reader.IsDBNull(reader.GetOrdinal("PrecioVenta")) ? null : new PrecioVenta(reader.GetDecimal(reader.GetOrdinal("PrecioVenta")))
                    };
                    products.Add(product);
                }

                return products;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException($"Error al listar productos en la base de datos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error inesperado al listar productos: {ex.Message}", ex);
            }
        }
    }
}
