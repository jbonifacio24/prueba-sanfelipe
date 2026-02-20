using MS.Compra.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.ValueObjects
{
    public class TotalCab
    {
        public decimal totalCab { get; private set; }
        public TotalCab(decimal total)
        {
            if (total < 0)
                throw new DomainException("El total no puede ser negativo.");
            this.totalCab = total;
        }

        // Constructor sin parámetros SOLO para EF Core
        private TotalCab() { }
    }
}
