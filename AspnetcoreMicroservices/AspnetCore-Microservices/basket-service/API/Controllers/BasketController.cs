using API.Entities;
using API.GrpcServices;
using API.Repositories.interfaces;
using AutoMapper;
using EventBus.Messages.Events.Basket;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<BasketController> _logger;
        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IMapper mapper, IPublishEndpoint publishEndpoint, ILogger<BasketController> logger)
        {
            _discountGrpcService = discountGrpcService;
            _basketRepository = basketRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
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

        [HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _basketRepository.GetBasket(basketCheckout.UserName);
            if (basket == null)
                return BadRequest();
            var eventMessage = _mapper.Map<AddBasketItemToCheckoutEvent>(basketCheckout);
            var message = JsonConvert.SerializeObject(eventMessage);
            _logger.LogInformation(message);
            eventMessage.UpdatePrice(basket.TotalPrice);
            await _publishEndpoint.Publish(eventMessage);
            await _basketRepository.DeleteBasket(basket.UserName); 
            return Accepted();
        }
    }
}
