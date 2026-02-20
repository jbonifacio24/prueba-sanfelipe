using MS.Compra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.Interfaces
{
    public interface ICreateOrderRepository
    {
        public Task<int> CreateOrderAsync(OrderCab sale);
        
    }
}
