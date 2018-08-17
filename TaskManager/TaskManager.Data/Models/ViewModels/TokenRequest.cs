using System.ComponentModel.DataAnnotations;

namespace TaskManager.Data.Models.ViewModels
{
    public class TokenRequest
    {
        [Required]
        public string grant_type { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
