using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Producto.Domain.Entities;
using MS.Producto.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MS.Producto.Infrastucture.Decorators
{
    public class LoggingProductRepositoryDecorator : ICreateProductRepository
    {
        private readonly ICreateProductRepository _inner;
        private readonly ILogger<LoggingProductRepositoryDecorator> _logger;

        public LoggingProductRepositoryDecorator(ICreateProductRepository inner, ILogger<LoggingProductRepositoryDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task CreateProductAsync(Product input)
        {
            _logger.LogInformation("Iniciando registro de producto...");
            await _inner.CreateProductAsync(input);
            _logger.LogInformation("Producto registrado correctamente.");
        }
    }
}
