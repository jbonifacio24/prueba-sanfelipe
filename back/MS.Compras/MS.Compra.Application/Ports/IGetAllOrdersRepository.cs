using MS.Compra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Application.Ports
{
    internal interface IGetAllOrdersRepository
    {
        public Task<List<OrderCab>> GetAllOrdersAsync();
    }
}
