using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistences;
using Ordering.Application.Models;
using Ordering.Application.Services.Interfaces;
using Ordering.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand : IRequest<Guid>
    {
        public string UserName { get;  set; }
        public decimal TotalPrice { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string EmailAddress { get;  set; }
        public string AddressLine { get;  set; }
        public string Country { get;  set; }
        public string State { get;  set; }
        public string ZipCode { get;  set; }
        public string CardName { get;  set; }
        public string CardNumber { get;  set; }
        public string Expiration { get;  set; }
        public string CVV { get;  set; }
        public int PaymentMethod { get;  set; }
    }

    public class CheckoutCommandHandler : IRequestHandler<CheckoutOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutCommandHandler> _logger;

        public CheckoutCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckoutCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Ordering.Domain.Aggregates.Order.Order>(request);
            var newOrder = await _orderRepository.AddAsync(order);
            return newOrder.Id;
        }

        //private async Task SendMail(Ordering.Domain.Aggregates.Order.Order order)
        //{
        //    var email = new Email
        //    {
        //        To = "huy2002109@gmail.com",
        //        Body = "Order was created",
        //        Subject = "Order was created by huy2002"
        //    };
        //    await _emailService.SendMail(email);
        //}
    }
}
