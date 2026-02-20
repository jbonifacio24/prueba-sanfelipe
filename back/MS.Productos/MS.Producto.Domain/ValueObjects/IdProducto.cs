using MS.Producto.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Domain.ValueObjects
{
    public class IdProducto
    {
        public int Id { get; private set; }
        public IdProducto(int id)
        {
            if (id < 0)
                throw new DomainException("El ID del producto debe ser mayor de cero.");
            this.Id = id;
        }
    }
}
