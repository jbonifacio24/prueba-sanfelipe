using MS.Producto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Domain.Interfaces
{
    public interface ICreateProductRepository
    {
     
        public Task CreateProductAsync(Product input)
        {
            return Task.CompletedTask;
        }
    }
}
