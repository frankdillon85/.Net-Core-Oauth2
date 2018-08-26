
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.DB.Models;
using TaskManager.Service.Interface;

namespace TaskManager.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]User user)
        {
            var existingUser = await _userService.FindByEmail(user.Email);

            if (existingUser == null)
            {
                user.Password = _userService.HashPassword(user.Password);
                user.IsActive = true;

                try
                {
                    await _userService.AddAsync(user);
                    await _userService.SaveAsync();

                    return Ok(user);
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            return StatusCode(409, "Email already exists");
        }
    }
}