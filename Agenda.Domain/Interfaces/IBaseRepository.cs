using Agenda.Domain.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Agenda.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : Register
    {
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(T model);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> filter = null,
                                         Func<IQueryable<T> , IIncludableQueryable<T, object>> include = null, bool asNoTracking = false, int? skip = null, int? take = null);
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null, bool asNoTracking = true);
    }
}
