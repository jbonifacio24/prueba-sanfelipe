using MS.Movimiento.Domain.Entities;
using MS.Movimiento.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MS.Movimiento.Infrastructure.Decorators
{
    public class LoggingMovementRepositoryDecorator : ICreateMovementRepository
    {
        private readonly ICreateMovementRepository _inner;
        private readonly ILogger<LoggingMovementRepositoryDecorator> _logger;
        public LoggingMovementRepositoryDecorator(ICreateMovementRepository inner, ILogger<LoggingMovementRepositoryDecorator> logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public async Task CreateMovementAsync(MovementCab input)
        {
            _logger.LogInformation("Iniciando registro de la compra...");
            await _inner.CreateMovementAsync(input);
            _logger.LogInformation("Compra registrado correctamente.");
        }
    }
}
