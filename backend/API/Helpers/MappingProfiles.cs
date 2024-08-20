using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Core.Entities.Identity.Address, AddressDTO>().ReverseMap();
            CreateMap<AddressDTO, Core.Entities.OrderAggregate.Address>();

            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();

            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(
                    o => o.DeliveryMethodName,
                    o => o.MapFrom(s => s.DeliveryMethod.ShortName)
                )
                .ForMember(o => o.DeliveryPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(
                    oid => oid.ProductName,
                    oid => oid.MapFrom(o => o.ProductItemOrder.ProductName)
                )
                .ForMember(
                    oid => oid.PictureUrl,
                    oid => oid.MapFrom(o => o.ProductItemOrder.PictureUrl)
                )
                .ForMember(oid => oid.PictureUrl, oid => oid.MapFrom<OrderItemUrlResolver>());
        }
    }
}
