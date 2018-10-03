using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Service.Interface;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Controllers
{
    [Route("api/token")]
    public class TokenController : ControllerBase
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
                try
                {
                    var accessToken = await _accessToken.GetAccessToken(model);
                    if (accessToken.statusCode == HttpStatusCode.OK)
                    {
                        return Ok(accessToken.message);
                    }

                    return StatusCode((int)accessToken.statusCode, accessToken.message);
                }
                catch(Exception)
                {
                    return StatusCode(500, "An error occurred while authenticating");
                }
            }
            else
            {
                return new BadRequestResult();
            }
        }


    }
}

