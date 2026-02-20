using MS.Compra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Application.UseCases
{
    public record CreateOrderCabCommand
    {
        public decimal? SubTotal { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }
        public List<CreateOrderDetCommand> det { get; set; }
}
}
