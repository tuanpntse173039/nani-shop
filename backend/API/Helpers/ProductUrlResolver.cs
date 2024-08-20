using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(
            Product source,
            ProductDTO destination,
            string destMember,
            ResolutionContext context
        )
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.PictureUrl;
            }
            return string.Empty;
        }
    }
}
