using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data.DB.Models;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        bool VerifyPassword(string password, string passwordHash);
        string HashPassword(string password);
        Task<UserViewModel> GetByIdAsync(Guid id);
        Task<AuthUserViewModel> FindByEmail(string email);
        Task<IEnumerable<UserViewModel>> FindWhere(Expression<Func<User, bool>> predicate);
        Task AddAsync(AuthUserViewModel entity);
        Task<UserViewModel> UpdateAsync(int id, UserViewModel entity);
        Task SaveAsync();
        void Dispose();
    }
}
