using MS.Compra.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.ValueObjects
{
    public class IgvDet
    {
        public decimal igvdet { get; private set; }
        public IgvDet(decimal igv)
        {
            if (igv < 0)
                throw new DomainException("El IGV no puede ser negativo.");
            this.igvdet = igv;
        }
        // Constructor sin parámetros SOLO para EF Core
        private IgvDet() { }
    }
}
