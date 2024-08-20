using System.Reflection;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductBrand> ProductBrands { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var propertiesDecimal = entityType
                        .ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in propertiesDecimal)
                    {
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion<double>();
                    }

                    var propertiesDatetimeOffset = entityType
                        .ClrType.GetProperties()
                        .Where(p =>
                            p.PropertyType == typeof(DateTimeOffset)
                            || p.PropertyType == typeof(DateTimeOffset?)
                        );
                    foreach (var property in propertiesDatetimeOffset)
                    {
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}
