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
    public class LoggingGetAllOrdersRepositoryDecorator : IGetAllOrdersRepository
    {
        private readonly IGetAllOrdersRepository _inner;
        private readonly ILogger<LoggingGetAllOrdersRepositoryDecorator> _logger;
        public LoggingGetAllOrdersRepositoryDecorator(IGetAllOrdersRepository inner, ILogger<LoggingGetAllOrdersRepositoryDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task<List<OrderCab>> GetAllOrdersAsync()
        {
            _logger.LogInformation("Iniciando registro de la compra...");
            var result = await _inner.GetAllOrdersAsync();
            _logger.LogInformation("Compra registrada correctamente.");
            return result;
        }
    }
}
