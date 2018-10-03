using AutoMapper;
using System.Collections.Generic;
using Oauth.Data.DB.Models;
using Oauth.Shared.DTO;

namespace Oauth.Service.Mappings
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, AuthUserDTO>();
            CreateMap<IEnumerable<User>, IEnumerable<UserDTO>>();
        }
    }
}
