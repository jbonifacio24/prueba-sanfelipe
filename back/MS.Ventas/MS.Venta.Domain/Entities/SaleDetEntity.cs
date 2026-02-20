using MS.Venta.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.Entities
{
    [Table("VentaDet")]
    public class SaleDetEntity
    {
        public int IdVentaDet { get; set; }
        public int IdVentaCab { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }
    }
}
