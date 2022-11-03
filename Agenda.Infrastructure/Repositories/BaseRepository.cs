using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Agenda.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Register
    {
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationContext context)
        {
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T model)
        {
            _dbSet.Add(model);

            return model;
        }
        public virtual async Task<T> UpdateAsync(T model)
        {
            model.UpdatedAt = DateTime.Now;
            _dbSet.Update(model);

            return model;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var obj = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            _dbSet.Remove(obj);
        }

        public virtual async Task<T> GetByIdAsync(int id,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dbSet.AsQueryable();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = false, int? skip = null, int? take = null)
        {
            var query = _dbSet.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (include != null)
                query = include(query);

            if (asNoTracking)
                query = query.AsNoTracking();

            if (skip != null && skip.HasValue)
                query = query.Skip(skip.Value);

            if (take != null && take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> filter = null, bool asNoTracking = true)
        {
            var count = await GetAllAsync(filter, asNoTracking: asNoTracking);
            return count.Count();
        }
    }
}
