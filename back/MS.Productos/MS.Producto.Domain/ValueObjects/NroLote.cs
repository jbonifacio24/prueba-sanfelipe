using MS.Producto.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Domain.ValueObjects
{
    public class NroLote
    {
        public int nroLote { get; private set; }
        public NroLote( int number) {
            if (number <= 0)
                throw new DomainException("El número de lote debe ser mayor que cero.");
            this.nroLote = number;

        }
    }
}
