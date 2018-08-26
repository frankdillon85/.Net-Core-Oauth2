using System;
using System.Collections.Generic;

namespace TaskManager.Data.DB.Models
{
    public partial class User
    {
        public Guid UserId { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
    }
}
