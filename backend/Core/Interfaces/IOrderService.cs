using Core.Entities.OrderAggregate;

namespace Core.Interfaces;

public interface IOrderService
{
    Task<Order?> CreateOrderAsync(
        string buyerEmail,
        int deliveryMethod,
        string basketId,
        Address shippinAddress
    );
    Task<IReadOnlyList<Order>> GetOrdersByUserAsync(string buyerEmail);
    Task<Order?> GetOrderByIdAsync(int orderId, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
}
