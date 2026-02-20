using MS.Compra.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.ValueObjects
{
    public class SubTotalDet
    {
        public decimal subTotalDet { get; private set; }
        public SubTotalDet(decimal subTotal)
        {
            if (subTotal < 0)
                throw new DomainException("El subtotal no puede ser negativo.");
            this.subTotalDet = subTotal;
        }

        // Constructor sin parámetros SOLO para EF Core
        private SubTotalDet() { }
    }
}
