
using MS.Movimiento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Movimiento.Domain.Interfaces
{
    public interface ICreateMovementRepository
    {
        public Task CreateMovementAsync(MovementCab input)
        {
            return Task.CompletedTask;
        }
    }
}
