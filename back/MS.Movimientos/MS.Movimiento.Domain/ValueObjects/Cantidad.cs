using MS.Movimiento.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Movimiento.Domain.ValueObjects
{
    public class Cantidad
    {
        public int cantidad { get; private set; }
        public Cantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new DomainException("La cantidad debe ser mayor a cero.");
            this.cantidad = cantidad;
        }
    }
}
