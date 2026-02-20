using MS.Producto.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Domain.Entities
{
    public class Product
    {
        public int IdProducto { get; set; }
        public Costo? Costo { get; set; }
        public DateTime FecRegistro { get; set; }   
        public NombreProducto? NombreProducto { get; set; }
        public NroLote? NroLote { get; set; }
        public PrecioVenta? PrecioVenta { get; set; }

    }
}
