using MS.Venta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.Services
{
    public class CreateSaleService
    {
        public void CreateSale(SaleCab input) {
            if (input == null) throw new ArgumentNullException(nameof(input), "La venta no puede ser nulo.");

        }
    }
}
