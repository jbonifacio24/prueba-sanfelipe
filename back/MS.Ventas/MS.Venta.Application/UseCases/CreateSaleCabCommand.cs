using MS.Venta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Application.UseCases
{
    public record CreateSaleCabCommand
    {
        public int IdVentaCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public List<CreateSaleDetCommand> det { get; set; }
}
}
