using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oauth.Data.Interface
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> FindWhere(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task SaveAsync();
    }
}
