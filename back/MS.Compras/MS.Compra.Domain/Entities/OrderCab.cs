using MS.Compra.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.Entities
{
    public class OrderCab
    {
        public int IdCompraCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public SubTotalCab? SubTotalCab { get; set; }
        public IgvCab? IgvCab { get; set; }
        public TotalCab? TotalCab { get; set; }
        public List<OrderDet> OrderDet { get; set; }
    }
}
