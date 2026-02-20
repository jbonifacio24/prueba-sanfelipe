using MS.Venta.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.ValueObjects
{
    public class PrecioDet
    {
        public decimal precioDet { get; private set; }
        public PrecioDet(decimal precio)
        {
            if (precio < 0)
                throw new DomainException("El precio no puede ser negativo.");
            this.precioDet = precio;
        }

        // Constructor sin parámetros SOLO para EF Core
        private PrecioDet() { }
    }
}
