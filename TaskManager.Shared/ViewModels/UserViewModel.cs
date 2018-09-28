using System;

namespace TaskManager.Shared.ViewModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
