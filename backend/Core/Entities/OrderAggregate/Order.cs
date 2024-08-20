namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order() { }

        public Order(
            string buyerEmail,
            Address address,
            DeliveryMethod deliveryMethod,
            IReadOnlyList<OrderItem> orderItems,
            decimal subTotal
        )
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = address;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public IReadOnlyList<OrderItem> OrderItems { get; set; } = null!;
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}
