using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistences;
using Ordering.Application.Models.ViewModels;
using Ordering.Domain.Aggregates.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
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

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Guid>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id);
            if (order is null)
                return Guid.Empty;
            var orderUpdated = _mapper.Map<Order>(request);
            _repository.Update(orderUpdated);
            return orderUpdated.Id;
        }
    }
}
