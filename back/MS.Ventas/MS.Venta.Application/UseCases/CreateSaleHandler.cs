using MS.Venta.Domain.Entities;
using MS.Venta.Domain.Exceptions;
using MS.Venta.Domain.Interfaces;
using MS.Venta.Domain.Services;
using MS.Venta.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Application.UseCases
{
    public class CreateSaleHandler
    {
        private readonly ICreateSaleRepositoryEF _repository;
        private readonly CreateSaleService _service;

        public CreateSaleHandler(ICreateSaleRepositoryEF repository, CreateSaleService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<int> HandleAsync(CreateSaleCabCommand input)
        {
            try
            {
                var saleCab = new SaleCab
                {
                    IdVentaCab = input.IdVentaCab,
                    FecRegistro = input.FecRegistro,
                    SubTotalCab = new SubTotalCab(input.SubTotal),
                    IgvCab = new IgvCab(input.Igv),
                    TotalCab = new TotalCab(input.Total),
                    SaleDet = input.det.Select(d => new SaleDet
                    {
                        IdVentaDet = d.IdVentaDet,
                        IdVentaCab = d.IdVentaCab,
                        IdProductoDet = new IdProductoDet(d.IdProductoDet),
                        CantidadDet = new CantidadDet(d.CantidadDet),
                        PrecioDet = new PrecioDet(d.PrecioDet),
                        SubTotalDet = new SubTotalDet(d.SubTotalDet),
                        IgvDet = new IgvDet(d.IgvDet),
                        TotalDet = new TotalDet(d.TotalDet)
                    }).ToList()
                };

                

                //validar reglas de negocio antes de crear el producto
                _service.CreateSale(saleCab);

                // Si la validación es exitosa, se procede a crear el producto en el repositorio
                await _repository.CreateSaleEFAsync(saleCab);

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
                throw new ApplicationException($"Error inesperado al crear el producto: {ex.Message}", ex);
            }

        }
    }
}
