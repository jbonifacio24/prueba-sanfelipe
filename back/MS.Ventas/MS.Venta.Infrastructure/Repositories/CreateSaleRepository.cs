using MS.Venta.Domain.Entities;
using MS.Venta.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Infrastructure.Repositories
{
    public class CreateSaleRepository : ICreateSaleRepositoryEF
    {
        private readonly string _connectionString;
        public CreateSaleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }


        public async Task CreateSaleAsync(SaleCab input)
        {
            try
            {
                
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
