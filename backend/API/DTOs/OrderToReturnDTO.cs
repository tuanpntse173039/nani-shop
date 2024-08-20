using Core.Entities.OrderAggregate;

namespace API.DTOs;

public class OrderToReturnDTO
{
    public int Id { get; set; }
    public string? BuyerEmail { get; set; }
    public DateTimeOffset? OrderDate { get; set; }
    public Address? ShipToAddress { get; set; }
    public string? DeliveryMethodName { get; set; }
    public double DeliveryPrice { get; set; }
    public IReadOnlyList<OrderItemDTO> OrderItems { get; set; } = null!;
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string? Status { get; set; }
}
