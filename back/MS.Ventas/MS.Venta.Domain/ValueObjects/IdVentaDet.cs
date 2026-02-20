using MS.Venta.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.ValueObjects
{
    public class IdVentaDet
    {
        public int idVentaDet { get; private set; }
        public IdVentaDet(int idVentaDet)
        {
            if (idVentaDet <= 0)
                throw new DomainException("El ID del detalle de venta debe ser mayor a cero.");
            this.idVentaDet = idVentaDet;
        }
    }
}
