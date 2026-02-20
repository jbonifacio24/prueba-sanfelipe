using MS.Venta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Infrastructure.Mappers
{
    public class SaleCabMapper
    {
        public static SaleCabEntity ToEntity(SaleCab cab)
        {
            return new SaleCabEntity
            {
                IdVentaCab = cab.IdVentaCab,
                FecRegistro = cab.FecRegistro,
                SubTotal = cab.SubTotalCab?.subTotalCab,
                Igv = cab.IgvCab?.igv,
                Total = cab.TotalCab?.totalCab,
                SaleDet = cab.SaleDet?.Select(ToEntity).ToList()
            };
        }

        public static SaleDetEntity ToEntity(SaleDet det)
        {
            return new SaleDetEntity
            {
                IdVentaDet = det.IdVentaDet,
                IdVentaCab = det.IdVentaCab,
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
