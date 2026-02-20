using MS.Compra.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.Entities
{
    [Table("CompraDet")]
    public class OrderDetEntity
    {
        public int IdCompraDet { get; set; }
        public int IdCompraCab { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }
    }
}
