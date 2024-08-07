using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecification(ProductSpecParams productParams)
            : base(p =>
                (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId)
                && (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId)
                && (
                    string.IsNullOrEmpty(productParams.Search)
                    || p.Name.ToLower().Contains(productParams.Search)
                )
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name);
            ApplyPaging(
                productParams.PageSize * (productParams.PageIndex - 1),
                productParams.PageSize
            );

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypeAndBrandSpecification(int id)
            : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
