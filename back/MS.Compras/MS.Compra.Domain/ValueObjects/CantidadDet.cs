using MS.Compra.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.ValueObjects
{
    public class CantidadDet
    {
        public int cantidad { get; private set; }
        public CantidadDet(int cantidad)
        {
            if (cantidad <= 0)
                throw new DomainException("La cantidad debe ser mayor a cero.");
            this.cantidad = cantidad;
        }
    }
}
