using MS.Compra.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.ValueObjects
{
    public class SubTotalCab
    {
        public decimal subTotalCab { get; private set; }
        public SubTotalCab(decimal subTotal)
        {
            if (subTotal < 0)
                throw new DomainException("El subtotal no puede ser negativo.");
            this.subTotalCab = subTotal;
        }
        // Constructor sin parámetros SOLO para EF Core
        private SubTotalCab() { }
    }
}
