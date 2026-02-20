using MS.Compra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.Interfaces
{
    public interface IGetAllOrdersRepository
    {
        Task<List<OrderCab>> GetAllOrdersAsync();
    }
}
