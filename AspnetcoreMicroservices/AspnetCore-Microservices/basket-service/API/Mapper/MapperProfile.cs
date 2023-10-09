using API.Entities;
using AutoMapper;
using EventBus.Messages.Events.Basket;

namespace API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BasketCheckout, AddBasketItemToCheckoutEvent>().ReverseMap();
        }
    }
}
