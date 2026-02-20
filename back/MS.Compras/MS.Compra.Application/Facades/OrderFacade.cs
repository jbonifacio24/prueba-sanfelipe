using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Compra.Application.UseCases;
using MS.Compra.Domain.Entities;

namespace MS.Compra.Application.Facades
{
    public class OrderFacade
    {
        private readonly CreateOrderHandler _createHandler;
        private readonly GetAllOrdersHandler _getAllOrdersHanler;

        public OrderFacade(CreateOrderHandler createHandler, GetAllOrdersHandler getAllOrdersHanler)
        {
            _createHandler = createHandler;
            _getAllOrdersHanler = getAllOrdersHanler;
        }

        public async Task<int> CreateOrderAsync(CreateOrderCabCommand command)
        {
             return await _createHandler.HandleAsync(command);
        }

        public async Task<List<OrderCab>> GetAllOrdersAsync()
        {
            return await _getAllOrdersHanler.HandleAsync();
        }
    }
}
