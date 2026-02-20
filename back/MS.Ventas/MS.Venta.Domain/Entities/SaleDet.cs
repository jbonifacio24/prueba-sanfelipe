using MS.Venta.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.Entities
{
    public class SaleDet
    {
        public int IdVentaDet { get; set; }
        public int IdVentaCab { get; set; }
        public IdProductoDet? IdProductoDet { get; set; }
        public CantidadDet? CantidadDet { get; set; }
        public PrecioDet? PrecioDet { get; set; }
        public SubTotalDet? SubTotalDet { get; set; }
        public IgvDet? IgvDet { get; set; }
        public TotalDet? TotalDet { get; set; }

        
    }
}
