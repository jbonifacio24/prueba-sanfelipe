using MS.Venta.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.ValueObjects
{
    public class IdProductoDet
    {
        public int idProducto { get; private set; }
        public IdProductoDet(int idProducto)
        {
            if (idProducto <= 0)
                throw new DomainException("El ID del producto debe ser mayor a cero.");
            this.idProducto = idProducto;
        }
    }
}
