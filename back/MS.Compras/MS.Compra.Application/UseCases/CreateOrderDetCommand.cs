using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Application.UseCases
{
    public record CreateOrderDetCommand
    {
        public int? IdProductoDet { get; set; }
        public int? CantidadDet { get; set; }
        public decimal? PrecioDet { get; set; }
        public decimal? SubTotalDet { get; set; }
        public decimal? IgvDet { get; set; }
        public decimal?   TotalDet { get; set; }
    }
}
