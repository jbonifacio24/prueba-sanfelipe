using MS.Producto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Application.Ports
{
    public interface ICreateProductRepository
    {
         public Task CreateProductAsync(Product input);
    }
}
