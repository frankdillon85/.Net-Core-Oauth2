using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data.DB.Models;

namespace TaskManager.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        bool VerifyPassword(string password, string passwordHash);
        string HashPassword(string password);
        Task<User> GetByIdAsync(Guid id);
        Task<User> FindByEmail(string email);
        Task<IEnumerable<User>> FindWhere(Expression<Func<User, bool>> predicate);
        Task AddAsync(User entity);
        Task<User> UpdateAsync(int id, User entity);
        Task SaveAsync();
        void Dispose();
    }
}
