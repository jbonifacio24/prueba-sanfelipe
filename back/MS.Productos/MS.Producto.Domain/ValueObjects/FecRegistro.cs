using MS.Producto.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Domain.ValueObjects
{
    public class FecRegistro
    {
        public DateTime fecha { get; private set; }
        public FecRegistro(DateTime fecha)
        {
            if (fecha > DateTime.Now)
                throw new DomainException("La fecha de registro no puede ser futura.");
            this.fecha = fecha;
        }
    }
}
