using MS.Compra.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.Entities
{
    public class OrderDet
    {
        public int IdCompraDet { get; set; }
        public int IdCompraCab { get; set; }
        public IdProductoDet? IdProductoDet { get; set; }
        public CantidadDet? CantidadDet { get; set; }
        public PrecioDet? PrecioDet { get; set; }
        public SubTotalDet? SubTotalDet { get; set; }
        public IgvDet? IgvDet { get; set; }
        public TotalDet? TotalDet { get; set; }

        
    }
}
