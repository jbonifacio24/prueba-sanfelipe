using MS.Compra.Domain.Entities;
using MS.Compra.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Compra.Domain.Services
{
 

    public class GetAllOrdersService
    {
        private readonly IGetAllOrdersRepository _repository;

        public GetAllOrdersService(IGetAllOrdersRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderCab>> GetAllOrdersAsync()
        {
            return await _repository.GetAllOrdersAsync();
        }
    }
}
