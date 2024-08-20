namespace API.DTOs;

public class OrderDTO
{
    public string BasketId { get; set; } = string.Empty;
    public int DeliveryMethodId { get; set; }
    public AddressDTO ShippingAddress { get; set; } = null!;
}
