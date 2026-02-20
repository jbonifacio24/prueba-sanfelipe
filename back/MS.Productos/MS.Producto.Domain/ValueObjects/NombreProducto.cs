using MS.Producto.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Domain.ValueObjects
{
    public class NombreProducto
    {
        public string name { get; private set; }
        public NombreProducto(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("El nombre del producto no puede ser vacío.");
            this.name = name;
        }
    }
}
