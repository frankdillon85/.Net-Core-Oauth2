﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data.DB.Models;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        bool VerifyPassword(string password, string passwordHash);
        string HashPassword(string password);
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<AuthUserDTO> FindByEmail(string email);
        Task<IEnumerable<UserDTO>> FindWhere(Expression<Func<User, bool>> predicate);
        Task AddAsync(AuthUserDTO entity);
        Task<UserDTO> UpdateAsync(int id, UserDTO entity);
        Task SaveAsync();
        void Dispose();
    }
}
