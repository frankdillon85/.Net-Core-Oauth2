using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data.DB.Models;
using TaskManager.Data.Interface;
using TaskManager.Service.Helpers;
using TaskManager.Service.Interface;

namespace TaskManager.Service.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(User entity)
        {
            await _repo.AddAsync(entity);
        }

        public async Task<IEnumerable<User>> FindWhere(Expression<Func<User, bool>> predicate)
        {
            return await _repo.FindWhere(predicate);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task SaveAsync()
        {
            await _repo.SaveAsync();
        }

        public async Task<User> UpdateAsync(int id, User entity)
        {
            return await _repo.UpdateAsync(id, entity);
        }

        public void Dispose()
        {
            _repo.Dispose();
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return PasswordHash.ValidatePassword(password, passwordHash);
        }

        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}
