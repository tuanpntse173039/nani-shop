using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        [Required]
        [MaxLength(180)]
        public string Description { get; set; } = String.Empty;

        public decimal Price { get; set; }

        [Required]
        public string PictureUrl { get; set; } = String.Empty;

        public ProductType ProductType { get; set; } = null!;
        public int ProductTypeId { get; set; }

        public ProductBrand ProductBrand { get; set; } = null!;
        public int ProductBrandId { get; set; }
    }

}
