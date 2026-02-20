using MS.Venta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Venta.Domain.Interfaces
{
    public interface ICreateSaleRepositoryEF
    {
        public Task CreateSaleEFAsync(SaleCab sale)
        {
            return Task.CompletedTask;
        }
    }
}
