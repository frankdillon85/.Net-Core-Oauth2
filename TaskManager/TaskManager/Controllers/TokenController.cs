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
            if (model == null || !ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            if (model.grant_type == "password")
            {
                var accessToken = await _accessToken.GetAccessToken(model);
                if (accessToken.Item2 == HttpStatusCode.OK)
                {
                    return Ok(accessToken.Item1);
                }

                return StatusCode((int)accessToken.Item2,accessToken.Item1);
            }
            else
            {
                return new BadRequestResult();
            }
        }


    }
}

