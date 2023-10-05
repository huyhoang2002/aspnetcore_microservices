using API.Entities;
using API.GrpcServices;
using API.Repositories.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _discountGrpcService = discountGrpcService;
            _basketRepository = basketRepository;
        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket([FromRoute] string username)
        {
            var basket = await _basketRepository.GetBasket(username);
            return Ok(basket ?? new ShoppingCart(username));
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToBasket([FromBody] ShoppingCart basket)
        {
            var result = await _basketRepository.AddProductToBasket(basket);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //TODO: Communicate with discount Grpc
            //TODO: Recaculate the price
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            var result = await _basketRepository.UpdateBasket(basket);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{username}")]
        public async Task<ActionResult> DeleteBasket([FromRoute] string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok();
        } 
    }
}
