namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem() { }

        public OrderItem(ProductItemOrder productItemOrder, decimal price, int quantity)
        {
            ProductItemOrder = productItemOrder;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder ProductItemOrder { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
