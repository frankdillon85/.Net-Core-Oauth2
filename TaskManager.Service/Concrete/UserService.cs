using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data.DB.Models;
using TaskManager.Data.Interface;
using TaskManager.Service.Helpers;
using TaskManager.Service.Interface;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Service.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task AddAsync(AuthUserViewModel entity)
        {
            var user = _mapper.Map<User>(entity);
            await _repo.AddAsync(user);
        }

        public async Task<IEnumerable<UserViewModel>> FindWhere(Expression<Func<User, bool>> predicate)
        {
            var result = await _repo.FindWhere(predicate);
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(result);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var result = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(result);

        }

        public async Task<UserViewModel> GetByIdAsync(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task SaveAsync()
        {
            await _repo.SaveAsync();
        }

        public async Task<UserViewModel> UpdateAsync(int id, UserViewModel entity)
        {
            var user = _mapper.Map<User>(entity);
            var result = await _repo.UpdateAsync(id, user);

            return _mapper.Map<UserViewModel>(result);
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
            return PasswordHash.HashPassword(password);
        }

        public async Task<AuthUserViewModel> FindByEmail(string email)
        {
            var result = await _repo.GetByEmail(email);
            return _mapper.Map<AuthUserViewModel>(result);
        }
    }
}
