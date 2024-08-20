using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    private string? GetUserEmailFromContext()
    {
        return HttpContext.User.RetreiveUserEmailFromPrincipal();
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
    {
        var email = GetUserEmailFromContext();
        if (email == null)
        {
            return Unauthorized(new ApiResponse(401));
        }
        var shippingAddress = _mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);
        var order = await _orderService.CreateOrderAsync(
            email,
            orderDTO.DeliveryMethodId,
            orderDTO.BasketId,
            shippingAddress
        );
        if (order == null)
        {
            return BadRequest(new ApiResponse(400));
        }
        return order;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> ListAllOrder()
    {
        var email = GetUserEmailFromContext();
        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized(new ApiResponse(401));
        }

        var order = await _orderService.GetOrdersByUserAsync(email);
        var orderToReturnDTO = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDTO>>(
            order
        );
        return Ok(orderToReturnDTO);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderToReturnDTO>> ListOrderById(int id)
    {
        var email = GetUserEmailFromContext();
        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized(new ApiResponse(401));
        }

        var order = await _orderService.GetOrderByIdAsync(id, email);
        System.Console.WriteLine(">>>>" + order);
        System.Console.WriteLine(">>>>" + id);
        if (order == null)
            return NotFound(new ApiResponse(404));
        return Ok(_mapper.Map<Order, OrderToReturnDTO>(order));
    }

    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethods()
    {
        return Ok(await _orderService.GetDeliveryMethodAsync());
    }
}
