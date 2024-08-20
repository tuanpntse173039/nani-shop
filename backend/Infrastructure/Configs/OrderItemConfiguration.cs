using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configs
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(
                oi => oi.ProductItemOrder,
                io =>
                {
                    io.WithOwner();
                }
            );

            builder.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
        }
    }
}
