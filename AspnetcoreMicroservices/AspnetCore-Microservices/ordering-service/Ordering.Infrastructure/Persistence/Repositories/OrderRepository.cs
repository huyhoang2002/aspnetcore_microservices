using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistences;
using Ordering.Domain.Aggregates.Order;
using Ordering.Infrastructure.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            return _context.Set<Order>().Where(_ => _.UserName == userName);
        }
    }
}
