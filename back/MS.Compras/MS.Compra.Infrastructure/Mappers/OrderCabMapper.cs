using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Compra.Domain.Entities;

namespace MS.Compra.Infrastructure.Mappers
{
    public class OrderCabMapper
    {
        public static OrderCabEntity ToEntity(OrderCab cab)
        {
            return new OrderCabEntity
            {
                SubTotal = cab.SubTotalCab?.subTotalCab,
                Igv = cab.IgvCab?.igv,
                Total = cab.TotalCab?.totalCab,
                OrderDet = cab.OrderDet?.Select(ToEntity).ToList()
            };
        }

        public static OrderDetEntity ToEntity(OrderDet det)
        {
            return new OrderDetEntity
            {
                IdCompraDet = det.IdCompraDet,
                IdCompraCab = det.IdCompraCab,
                IdProducto = det.IdProductoDet?.idProducto,
                Cantidad = det.CantidadDet?.cantidad,
                Precio = det.PrecioDet?.precioDet,
                SubTotal = det.SubTotalDet?.subTotalDet,
                Igv = det.IgvDet != null ? det.IgvDet.igvdet : 0m,
                Total = det.TotalDet?.totalDet
            };
        }
    }
}
