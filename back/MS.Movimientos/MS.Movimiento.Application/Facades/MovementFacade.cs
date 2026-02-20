using MS.Movimiento.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Movimiento.Application.Facades
{
    public class MovementFacade
    {
        private readonly CreateMovementHandler _createHandler;

        public MovementFacade(CreateMovementHandler createHandler)
        {
            _createHandler = createHandler;
        }

        public async Task CreateMovementAsync(CreateMovementCabCommand command)
        {
            await _createHandler.HandleAsync(command);
        }
    }
}
