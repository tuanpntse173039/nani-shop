using API.DTOs;
using AutoMapper;
using Core.Entities.OrderAggregate;

namespace API.Helpers;

public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
{
    private readonly IConfiguration _configuration;

    public OrderItemUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(
        OrderItem source,
        OrderItemDTO destination,
        string destMember,
        ResolutionContext context
    )
    {
        if (!string.IsNullOrEmpty(source.ProductItemOrder.PictureUrl))
        {
            return _configuration["ApiUrl"] + source.ProductItemOrder.PictureUrl;
        }
        return string.Empty;
    }
}
