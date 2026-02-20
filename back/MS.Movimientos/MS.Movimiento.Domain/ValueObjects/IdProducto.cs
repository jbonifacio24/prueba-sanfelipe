using MS.Movimiento.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Movimiento.Domain.ValueObjects
{
    public class IdProducto
    {
        public int idProducto { get; private set; }
        public IdProducto(int idProducto)
        {
            if (idProducto <= 0)
                throw new DomainException("El ID del producto debe ser mayor a cero.");
            this.idProducto = idProducto;
        }
    }
}
