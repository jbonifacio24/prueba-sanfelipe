using MS.Producto.Domain.Entities;
using MS.Producto.Domain.Interfaces;
using MS.Producto.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Application.UseCases
{
    public class GetAllProductsHandler
    {
        private readonly IGetAllProductsRepository _repository;
        private readonly GetAllProductsService _service;

        public GetAllProductsHandler(IGetAllProductsRepository repository, GetAllProductsService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<List<Product>> HandleAsync()
        {
            return await _repository.GetAllProductsAsync();
        }
    }
}
