using MS.Compra.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.ValueObjects
{
    public class TotalDet
    {
        public decimal totalDet { get; private set; }
        public TotalDet(decimal total)
        {
            if (total < 0)
                throw new DomainException("El total no puede ser negativo.");
            this.totalDet = total;
        }

        // Constructor sin parámetros SOLO para EF Core
        private TotalDet() { }
    }
}
