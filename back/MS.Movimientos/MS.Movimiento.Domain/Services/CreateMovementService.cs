using MS.Movimiento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Movimiento.Domain.Services
{
    public class CreateMovementService
    {
        public void CreateMovement(MovementCab input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input), "El movimiento no puede ser nulo.");

        }
    }
}
