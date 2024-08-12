using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDTO>> UpdateCustomerBasket(
            CustomerBasketDTO basketDTO
        )
        {
            var basket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basketDTO);
            var customerBasketUpdated = await _basketRepository.UpdateBasketAsync(basket);

            if (customerBasketUpdated == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(_mapper.Map<CustomerBasket, CustomerBasketDTO>(customerBasketUpdated));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomerBasket(string id)
        {
            var basket = await _basketRepository.DeleteBasketAsync(id);
            if (!basket)
                return BadRequest();
            return Ok();
        }
    }
}
