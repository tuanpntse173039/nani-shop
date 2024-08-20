using Core.Entities.OrderAggregate;

namespace Core.Specification;

public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Order>
{
    public OrderWithItemsAndOrderingSpecification(string buyerEmail)
        : base(o => o.BuyerEmail == buyerEmail)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
        AddOrderByDescending(o => o.OrderDate);
    }

    public OrderWithItemsAndOrderingSpecification(string buyerEmail, int id)
        : base(o => o.BuyerEmail == buyerEmail && o.Id == id)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
    }
}
