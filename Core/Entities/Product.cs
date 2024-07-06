using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = String.Empty;

        [Required]
        [MaxLength(180)]
        public string Description { get; set; } = String.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string PictureUrl { get; set; } = String.Empty;

        public ProductType? ProductType { get; set; }
        public int ProductTypeId { get; set; }

        public ProductBrand? ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }

}
