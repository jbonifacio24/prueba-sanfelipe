using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MS.Venta.Domain.Entities;

namespace MS.Venta.Infrastructure.Data
{
    public class SaleDbContext : DbContext
    {
        public SaleDbContext(DbContextOptions<SaleDbContext> options) : base(options) { }
        public DbSet<SaleCabEntity> SaleCabs { get; set; }
        public DbSet<SaleDetEntity> SaleDets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<SaleCabEntity>()
                .HasKey(s => s.IdVentaCab);

            modelBuilder.Entity<SaleDetEntity>()
        .HasKey(e => e.IdVentaDet);

            modelBuilder.Entity<SaleCabEntity>()
                .HasMany(s => s.SaleDet)
                .WithOne()
                .HasForeignKey(d => d.IdVentaCab);

            
        }

    }
}
