using MS.Venta.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.ValueObjects
{
    public class IgvCab
    {
        public decimal igv { get; private set; }
        public IgvCab(decimal igv)
        {
            if (igv < 0)
                throw new DomainException("El IGV no puede ser negativo.");
            this.igv = igv;
        }
    }
}
