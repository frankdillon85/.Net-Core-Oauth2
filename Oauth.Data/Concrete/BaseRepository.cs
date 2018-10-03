using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Oauth.Data.DB.Models;
using Oauth.Data.Interface;

namespace Oauth.Data.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        JPVDBContext _context;

        public BaseRepository(JPVDBContext context)
        {
            _context = context;
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindWhere(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            if (entity == null)
                return null;
            T exist = await _context.Set<T>().FindAsync(id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
            }
            return exist;
        }
    }
}
