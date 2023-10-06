using Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistences.Base
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
