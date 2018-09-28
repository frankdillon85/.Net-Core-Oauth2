using AutoMapper;
using System.Collections.Generic;
using TaskManager.Data.DB.Models;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Service.Mappings
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, AuthUserViewModel>();
            CreateMap<IEnumerable<User>, IEnumerable<UserViewModel>>();
        }
    }
}
