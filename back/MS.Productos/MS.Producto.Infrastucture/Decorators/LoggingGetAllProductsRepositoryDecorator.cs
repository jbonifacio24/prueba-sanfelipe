using MS.Producto.Domain.Entities;
using MS.Producto.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Infrastucture.Decorators
{
    public class LoggingGetAllProductsRepositoryDecorator : IGetAllProductsRepository
    {
        private readonly IGetAllProductsRepository _inner;
        private readonly ILogger<LoggingGetAllProductsRepositoryDecorator> _logger;
        public LoggingGetAllProductsRepositoryDecorator(IGetAllProductsRepository inner, ILogger<LoggingGetAllProductsRepositoryDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            _logger.LogInformation("Iniciando listado de productos...");
            var products = await _inner.GetAllProductsAsync();
            _logger.LogInformation("Listado de productos obtenido correctamente. Total: {Count}", products.Count);
            return products;
        }
    }
}
