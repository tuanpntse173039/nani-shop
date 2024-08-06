using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductBrand> ProductBrands { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasConversion<double>();
        }
    }
}
