using MS.Movimiento.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MS.Movimiento.Domain.Entities
{
    public class MovementCab
    {
        public IdTipoMovimiento? IdTipoMovimiento { get; set; }
        public IdDocumentoOrigen? IdDocumentoOrigen { get; set; }
        public List<MovementDet>? MovementDet { get; set; }
    }
}
