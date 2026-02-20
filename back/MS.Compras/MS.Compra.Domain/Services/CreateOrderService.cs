using MS.Compra.Domain.Entities;
using MS.Compra.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.Services
{
    public class CreateOrderService
    {
        public void CreateOrder(OrderCab input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input), "La compra no puede ser nulo.");

            foreach(var item in input.OrderDet)
            {
                var cantidad = item.CantidadDet?.cantidad ?? 0;
                var precio = item.PrecioDet?.precioDet ?? 0;

                var subTotal = cantidad * precio;
                var igv = subTotal * 0.18m;
                var total = subTotal + igv;

                item.SubTotalDet = new SubTotalDet(subTotal);
                item.IgvDet = new IgvDet(igv);
                item.TotalDet = new TotalDet(total);
            }

            var subTotalCab = input.OrderDet
                                    .Where(x => x.SubTotalDet != null)
                                    .Sum(x => x.SubTotalDet!.subTotalDet);

            input.SubTotalCab = new SubTotalCab(subTotalCab);

            var igvTotalCab = input.OrderDet
                                    .Where(x => x.IgvDet != null)
                                    .Sum(x => x.IgvDet!.igvdet);
            input.IgvCab = new IgvCab(igvTotalCab);

            var totalCab = subTotalCab + igvTotalCab;
        }
    }
}
