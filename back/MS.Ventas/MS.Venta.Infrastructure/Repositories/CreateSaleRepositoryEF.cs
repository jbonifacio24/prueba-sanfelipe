using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Venta.Domain.Entities;
using MS.Venta.Domain.Interfaces;
using MS.Venta.Infrastructure.Data;
using MS.Venta.Infrastructure.Mappers;

namespace MS.Venta.Infrastructure.Repositories
{
    public class CreateSaleRepositoryEF : ICreateSaleRepositoryEF
    {
        private readonly SaleDbContext _context;

        public CreateSaleRepositoryEF(SaleDbContext context)
        {
            _context = context;
        }

        public async Task CreateSaleEFAsync(SaleCab input)
        {
            var entity = SaleCabMapper.ToEntity(input);

            _context.SaleCabs.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
