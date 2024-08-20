namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public decimal Price { get; set; }

        public string PictureUrl { get; set; } = String.Empty;

        public ProductType ProductType { get; set; } = null!;
        public int ProductTypeId { get; set; }

        public ProductBrand ProductBrand { get; set; } = null!;
        public int ProductBrandId { get; set; }
    }
}
