using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(
                o => o.ShipToAddress,
                a =>
                {
                    a.WithOwner();
                }
            );
            builder
                .Property(o => o.Status)
                .HasConversion(
                    oS => oS.ToString(),
                    os => (OrderStatus)Enum.Parse(typeof(OrderStatus), os)
                );

            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
