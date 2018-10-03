using System;

namespace Oauth.Shared.DTO
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
