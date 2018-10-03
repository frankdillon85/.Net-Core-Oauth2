using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Oauth.Service.Interface;
using Oauth.Shared.DTO;

namespace Oauth.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var x = await _userService.GetAllAsync();
                return Ok(x);
            }
            catch(Exception)
            {
                return StatusCode(500, "Error retriving all users");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]AuthUserDTO user)
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