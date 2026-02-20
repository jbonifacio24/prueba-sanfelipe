using Microsoft.Extensions.Logging;
using MS.Compra.Domain.Entities;
using MS.Compra.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Infrastructure.Decorators
{
    public class LoggingOrderRepositoryDecorator : ICreateOrderRepository
    {
        private readonly ICreateOrderRepository _inner;
        private readonly ILogger<LoggingOrderRepositoryDecorator> _logger;
        public LoggingOrderRepositoryDecorator(ICreateOrderRepository inner, ILogger<LoggingOrderRepositoryDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<int> CreateOrderAsync(OrderCab input)
        {
            _logger.LogInformation("Iniciando registro de la compra...");
            var result = await _inner.CreateOrderAsync(input);
            _logger.LogInformation("Compra registrada correctamente.");
            return result;
        }
    }
}
