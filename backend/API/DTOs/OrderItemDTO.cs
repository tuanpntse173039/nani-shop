namespace API.DTOs;

public class OrderItemDTO
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
