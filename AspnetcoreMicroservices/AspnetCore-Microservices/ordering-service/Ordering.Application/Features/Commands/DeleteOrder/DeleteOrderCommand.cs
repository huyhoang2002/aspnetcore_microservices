using MediatR;
using Ordering.Application.Contracts.Persistences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Guid>
    {
        private readonly IOrderRepository _repository;

        public DeleteOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id);
            if (order == null)
            {
                return Guid.Empty;
            }
            _repository.Delete(order);
            return order.Id;
        }
    }
}
