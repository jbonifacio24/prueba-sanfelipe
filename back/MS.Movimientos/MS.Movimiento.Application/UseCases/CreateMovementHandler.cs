using MS.Movimiento.Application.UseCases;
using MS.Movimiento.Domain.Entities;
using MS.Movimiento.Domain.Exceptions;
using MS.Movimiento.Domain.Interfaces;
using MS.Movimiento.Domain.Services;
using MS.Movimiento.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Movimiento.Application.UseCases
{
    public class CreateMovementHandler
    {
        private readonly ICreateMovementRepository _repository;
        private readonly CreateMovementService _service;

        public CreateMovementHandler(ICreateMovementRepository repository, CreateMovementService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<int> HandleAsync(CreateMovementCabCommand input)
        {
            try
            {
                var movementCab = new MovementCab
                {
                    IdTipoMovimiento = new IdTipoMovimiento(input.IdTipoMovimiento),
                    IdDocumentoOrigen = new IdDocumentoOrigen(input.IdDocumentoOrigen),
                    MovementDet = input.det.Select(d => new MovementDet
                    {
                        IdProducto = new IdProducto(d.IdProducto),
                        Cantidad = new Cantidad(d.Cantidad),
                        
                    }).ToList()
                };

                

                //validar reglas de negocio antes de crear el producto
                _service.CreateMovement(movementCab);

                // Si la validación es exitosa, se procede a crear el producto en el repositorio
                await _repository.CreateMovementAsync(movementCab);

                return 1;
            }
            catch (DomainException)
            {

                throw;
            }
            catch (InvalidOperationException ioex)
            {
                // Excepción de infraestructura (por ejemplo, error de conexión)
                throw new ApplicationException($"Error de infraestructura: {ioex.Message}", ioex);
            }
            catch (Exception ex)
            {
                // Cualquier otro error inesperado
                throw new ApplicationException($"Error inesperado al crear la compra: {ex.Message}", ex);
            }

        }
    }
}
