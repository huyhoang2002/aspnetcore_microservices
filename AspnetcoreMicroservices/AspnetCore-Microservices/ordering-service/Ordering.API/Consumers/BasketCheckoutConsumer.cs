using AutoMapper;
using EventBus.Messages.Events.Basket;
using MassTransit;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Infrastructure.CQRSBus.Interfaces;

namespace Ordering.API.Consumers
{
    public class BasketCheckoutConsumer : IConsumer<AddBasketItemToCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly ICommandBus _commandBus;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMapper mapper, ICommandBus commandBus, ILogger<BasketCheckoutConsumer> logger)
        {
            _mapper = mapper;
            _commandBus = commandBus;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<AddBasketItemToCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            _logger.LogInformation(command.UserName);
            await _commandBus.SendAsync(command);
        }
    }
}
