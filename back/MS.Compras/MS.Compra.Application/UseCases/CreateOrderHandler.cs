using MS.Compra.Application.UseCases;
using MS.Compra.Domain.Entities;
using MS.Compra.Domain.Exceptions;
using MS.Compra.Domain.Interfaces;
using MS.Compra.Domain.Services;
using MS.Compra.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Application.UseCases
{
    public class CreateOrderHandler
    {
        private readonly ICreateOrderRepository _repository;
        private readonly CreateOrderService _service;

        public CreateOrderHandler(ICreateOrderRepository repository, CreateOrderService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<int> HandleAsync(CreateOrderCabCommand input)
        {
            try
            {
                var orderCab = new OrderCab
                {
                    SubTotalCab = new SubTotalCab(input.SubTotal ?? 0m),
                    IgvCab = new IgvCab(input.Igv ?? 0m),
                    TotalCab = new TotalCab(input.Total ?? 0m),
                    OrderDet = input.det.Select(d => new OrderDet
                    {
                        IdProductoDet = new IdProductoDet(d.IdProductoDet ?? 0),
                        CantidadDet = new CantidadDet(d.CantidadDet ?? 0),
                        PrecioDet = new PrecioDet(d.PrecioDet ?? 0m),
                        SubTotalDet = new SubTotalDet(d.SubTotalDet ?? 0m),
                        IgvDet = new IgvDet(d.IgvDet ?? 0m),
                        TotalDet = new TotalDet(d.TotalDet ?? 0m)
                    }).ToList()
                };

                

                //validar reglas de negocio antes de crear el producto
                _service.CreateOrder(orderCab);

                // Si la validación es exitosa, se procede a crear el producto en el repositorio
                return await _repository.CreateOrderAsync(orderCab);

                
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
