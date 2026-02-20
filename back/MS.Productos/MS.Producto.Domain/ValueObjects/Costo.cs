using MS.Producto.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Domain.ValueObjects
{
    public class Costo
    {
        public decimal amount { get; private set; }
        public Costo(decimal amount) {
            if (amount < 0)
                throw new DomainException("El costo no puede ser negativo.");
            this.amount = amount;
        }
    }
}
