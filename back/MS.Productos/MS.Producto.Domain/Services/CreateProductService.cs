using MS.Producto.Domain.Entities;
using MS.Producto.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Domain.Services
{
    public class CreateProductService
    {
        public void CreateProduct(Product input)
        {
            if (input == null) 
                throw new ArgumentNullException(nameof(input), "El producto no puede ser nulo.");

            // Regla de negocio: precio de venta = costo * 1.35
            var precioVentaCalculado = input.Costo?.amount * 1.35m ?? 0m;
            input.PrecioVenta = new PrecioVenta(precioVentaCalculado);


        }
    }
}
