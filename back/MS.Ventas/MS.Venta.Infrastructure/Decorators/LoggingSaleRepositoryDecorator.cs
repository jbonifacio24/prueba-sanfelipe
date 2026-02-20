using Microsoft.Extensions.Logging;
using MS.Venta.Domain.Entities;
using MS.Venta.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Infrastructure.Decorators
{
    public class LoggingSaleRepositoryDecorator : ICreateSaleRepositoryEF
    {
        private readonly ICreateSaleRepositoryEF _inner;
        private readonly ILogger<LoggingSaleRepositoryDecorator> _logger;
        public LoggingSaleRepositoryDecorator(ICreateSaleRepositoryEF inner, ILogger<LoggingSaleRepositoryDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task CreateSaleEFAsync(SaleCab input)
        {
            _logger.LogInformation("Iniciando registro de la venta...");
            await _inner.CreateSaleEFAsync(input);
            _logger.LogInformation("Venta registrado correctamente.");
        }
    }
}
