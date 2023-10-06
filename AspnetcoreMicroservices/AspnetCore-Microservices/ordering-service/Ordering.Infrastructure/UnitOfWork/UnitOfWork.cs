using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfwork
    {
        private readonly OrderContext _orderContext;

        public UnitOfWork(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public async Task<T> SaveChangeAsync<T>(Func<Task<T>> action)
        {
            var result = await action();
            await _orderContext.SaveChangesAsync();
            return result;
        }
    }
}
