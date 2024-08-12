using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        public List<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();
    }
}
