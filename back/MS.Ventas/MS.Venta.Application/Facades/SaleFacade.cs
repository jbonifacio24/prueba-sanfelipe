using MS.Venta.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Application.Facades
{
    public class SaleFacade
    {
        private readonly CreateSaleHandler _createHandler;

        public SaleFacade(CreateSaleHandler createHandler)
        {
            _createHandler = createHandler;
        }

        public async Task<int> CreateSaleAsync(CreateSaleCabCommand command)
        {
            return await _createHandler.HandleAsync(command);
        }
    }
}
