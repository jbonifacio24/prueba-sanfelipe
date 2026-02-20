using MS.Compra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Application.UseCases
{
    public record GetAllOrdersCabCommand
    {
        public int IdCompraCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public List<GetAllOrdersDetCommand> det { get; set; }
}
}
