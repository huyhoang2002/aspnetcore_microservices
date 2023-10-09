using AutoMapper;
using EventBus.Messages.Events.Basket;
using MassTransit;
using Ordering.Application.Features.Commands.CheckoutOrder;

namespace Ordering.API.Mapper
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {
            CreateMap<CheckoutOrderCommand, AddBasketItemToCheckoutEvent>().ReverseMap();
        }
    }
}
