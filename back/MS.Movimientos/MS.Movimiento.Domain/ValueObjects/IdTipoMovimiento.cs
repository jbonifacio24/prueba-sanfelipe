using MS.Movimiento.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Movimiento.Domain.ValueObjects
{
    public class IdTipoMovimiento
    {
        public int idTipoMovimiento { get; private set; }
        public IdTipoMovimiento(int idTipoMovimiento)
        {
            if (idTipoMovimiento != 1 && idTipoMovimiento != 2)
                throw new DomainException("El tipo de movimiento es incorrecto, debe ser  1: entrada 2:salida.");
            this.idTipoMovimiento = idTipoMovimiento;
        }
    }
}
