using MS.Venta.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.Entities
{
    [Table("VentaCAb")]
    public class SaleCabEntity
    {
        public int IdVentaCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }
        public List<SaleDetEntity>? SaleDet { get; set; }
    }
}
