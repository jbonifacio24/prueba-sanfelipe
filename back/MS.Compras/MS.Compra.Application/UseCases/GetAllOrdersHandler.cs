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
    public class GetAllOrdersHandler
    {
        private readonly IGetAllOrdersRepository _repository;
        private readonly GetAllOrdersService _service;

        public GetAllOrdersHandler(IGetAllOrdersRepository repository, GetAllOrdersService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<List<OrderCab>> HandleAsync()
        {
            try
            { 
                return await _repository.GetAllOrdersAsync();
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
