using MS.Producto.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Domain.ValueObjects
{
    public class PrecioVenta
    {
        public decimal amount { get; private set; }
        public PrecioVenta(decimal amount)
        {
            if (amount < 0)
                throw new DomainException("El precio de venta no puede ser negativo.");
            this.amount = amount;
        }
    }
}
