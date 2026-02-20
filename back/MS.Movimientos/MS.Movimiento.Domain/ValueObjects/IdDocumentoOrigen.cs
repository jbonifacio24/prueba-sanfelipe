using MS.Movimiento.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Movimiento.Domain.ValueObjects
{
    public class IdDocumentoOrigen
    {
        public int idDocumentoOrigen { get; private set; }
        public IdDocumentoOrigen(int idDocumentoOrigen)
        {
            if (idDocumentoOrigen <= 0)
                throw new DomainException("El IDd documento origen debe ser mayor a cero.");
            this.idDocumentoOrigen = idDocumentoOrigen;
        }
    }
}
