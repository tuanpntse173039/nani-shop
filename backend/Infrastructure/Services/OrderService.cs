using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
    {
        _basketRepo = basketRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Order?> CreateOrderAsync(
        string buyerEmail,
        int deliveryMethodId,
        string basketId,
        Address shippingAddress
    )
    {
        //1. Get basket items from basket
        var basket = await _basketRepo.GetBasketAsync(basketId);
        if (basket == null)
        {
            return null;
        }

        //2. Get Order Item from repo
        var orderItems = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            if (product == null)
            {
                return null;
            }
            var productItemOrder = new ProductItemOrder
            {
                ProductItemId = product.Id,
                ProductName = product.Name,
                PictureUrl = product.PictureUrl
            };
            var orderItem = new OrderItem
            {
                ProductItemOrder = productItemOrder,
                Price = product.Price,
                Quantity = item.Quantity,
            };
            orderItems.Add(orderItem);
        }

        //3. Get Delivery Method from repo
        var deliveryMethod = await _unitOfWork
            .Repository<DeliveryMethod>()
            .GetByIdAsync(deliveryMethodId);
        if (deliveryMethod == null)
        {
            return null;
        }

        //4. Calculate subtotal
        var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

        //5. Create order
        var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subtotal);
        _unitOfWork.Repository<Order>().Add(order);

        //6. Save order to db
        var result = await _unitOfWork.Complete();
        if (result <= 0)
        {
            return null;
        }

        //7. Remove basket
        await _basketRepo.DeleteBasketAsync(basketId);

        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
    {
        return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId, string buyerEmail)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail, orderId);
        return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersByUserAsync(string buyerEmail)
    {
        var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);
        return await _unitOfWork.Repository<Order>().ListAsync(spec);
    }
}
