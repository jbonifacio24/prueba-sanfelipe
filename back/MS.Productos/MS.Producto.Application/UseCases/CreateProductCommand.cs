using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Producto.Application.UseCases
{
    public record CreateProductCommand
    {
        public int IdProducto { get; init; }
        public string? NombreProducto { get; init; }
        public int? NroLote { get; init; }
        public DateTime FecRegistro { get; init; }
        public decimal Costo { get; init; }
        public decimal PrecioVenta { get; init; }
    }
}
