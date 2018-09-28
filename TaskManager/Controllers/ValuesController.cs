using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.Service.Interface;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        IUserService _userService;

        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }
        // GET api/values
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var x = await _userService.GetAllAsync();
            return Ok(x);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
