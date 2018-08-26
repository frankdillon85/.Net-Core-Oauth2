using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Models.ViewModels;
using TaskManager.Models;
using TaskManager.Service.Interface;

namespace TaskManager.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private IAccessTokenService _accessToken;

        public TokenController(IAccessTokenService accessToken)
        {
            _accessToken = accessToken;
        }

        [ValidateModel]
        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody]TokenRequest model)
        {
            if (model.grant_type == "password")
            {
                var accessToken = await _accessToken.GetAccessToken(model);
                if (accessToken.statusCode == HttpStatusCode.OK)
                {
                    return Ok(accessToken.message);
                }

                return StatusCode((int)accessToken.statusCode, accessToken.message);
            }
            else
            {
                return new BadRequestResult();
            }
        }


    }
}

