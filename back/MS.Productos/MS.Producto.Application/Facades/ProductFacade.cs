using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Producto.Application.UseCases;
using MS.Producto.Domain.Entities;

namespace MS.Producto.Application.Facades
{
    public class ProductFacade
    {
        private readonly CreateProductHandler _createHandler;
        private readonly GetAllProductsHandler _getAllProductsHanler;
       

        public ProductFacade(CreateProductHandler createHandler, GetAllProductsHandler getAllProductsHanler)
        {
            _createHandler = createHandler;
            _getAllProductsHanler = getAllProductsHanler;
        }

        public async Task<int> CreateProductAsync(CreateProductCommand command)
        {
            return await _createHandler.HandleAsync(command);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _getAllProductsHanler.HandleAsync();
        }

    }
}
