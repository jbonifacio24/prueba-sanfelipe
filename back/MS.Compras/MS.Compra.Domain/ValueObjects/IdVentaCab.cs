using MS.Compra.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.ValueObjects
{
    public class IdVentaCab
    {
        public int idVentaCab { get; private set; }
        public IdVentaCab(int idVentaCab)
        {
            if (idVentaCab <= 0)
                throw new DomainException("El ID de la venta debe ser mayor a cero.");
            this.idVentaCab = idVentaCab;
        }
    }
}
