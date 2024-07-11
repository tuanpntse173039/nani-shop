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
        public ProductsWithTypeAndBrandSpecification(string sort)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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