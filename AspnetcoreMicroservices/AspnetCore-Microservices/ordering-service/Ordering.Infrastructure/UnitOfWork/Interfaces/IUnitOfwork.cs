using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.UnitOfWork.Interfaces
{
    public interface IUnitOfwork
    {
        Task<T> SaveChangeAsync<T>(Func<Task<T>> action);
    }
}
