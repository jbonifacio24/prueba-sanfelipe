using MS.Compra.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.Entities
{
    [Table("CompraCab")]
    public class OrderCabEntity
    {
        public int IdCompraCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }
        public List<OrderDetEntity>? OrderDet { get; set; }
    }
}
