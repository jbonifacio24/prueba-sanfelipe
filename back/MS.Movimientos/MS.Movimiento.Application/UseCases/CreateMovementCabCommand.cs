using MS.Movimiento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Movimiento.Application.UseCases
{
    public record CreateMovementCabCommand
    {
        public int IdTipoMovimiento { get; set; }
        public int IdDocumentoOrigen { get; set; }
        public List<CreateMovementDetCommand> det { get; set; }
}
}
