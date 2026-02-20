using MS.Venta.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.Entities
{
    public class SaleCab
    {
        public int IdVentaCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public SubTotalCab? SubTotalCab { get; set; }
        public IgvCab? IgvCab { get; set; }
        public TotalCab? TotalCab { get; set; }
        public List<SaleDet>? SaleDet { get; set; }
    }
}
