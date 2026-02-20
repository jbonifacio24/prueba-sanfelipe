using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Application.UseCases
{
    public record CreateSaleDetCommand
    {
        public int IdVentaDet { get; set; }
        public int IdVentaCab { get; set; }
        public int IdProductoDet { get; set; }
        public int CantidadDet { get; set; }
        public decimal PrecioDet { get; set; }
        public decimal SubTotalDet { get; set; }
        public decimal IgvDet { get; set; }
        public decimal TotalDet { get; set; }
    }
}
