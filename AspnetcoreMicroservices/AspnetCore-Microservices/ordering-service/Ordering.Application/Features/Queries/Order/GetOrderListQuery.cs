using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistences;
using Ordering.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Queries.Order
{
    public class GetOrderListQuery : IRequest<IEnumerable<OrderViewModel>>
    {
        public string UserName { get; set; }

        public GetOrderListQuery(string userName)        {
            UserName = userName;
        }
    }

    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IEnumerable<OrderViewModel>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderViewModel>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetOrderByUserName(request.UserName);
            return _mapper.Map<IEnumerable<OrderViewModel>>(result);
        }
    }
}
