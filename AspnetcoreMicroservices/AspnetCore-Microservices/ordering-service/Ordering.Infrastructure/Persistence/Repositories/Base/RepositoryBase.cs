using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistences.Base;
using Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence.Repositories.Base
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected OrderContext _context;
        public RepositoryBase(OrderContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {   if (predicate == null)
                return _context.Set<T>().AsNoTracking();
            return _context.Set<T>().Where(predicate);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(_ => _.Id == id);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
