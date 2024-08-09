using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasket(CustomerBasket basket)
        {
            var customerBasket = await _basketRepository.UpdateBasketAsync(basket);
            return Ok(customerBasket);
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
