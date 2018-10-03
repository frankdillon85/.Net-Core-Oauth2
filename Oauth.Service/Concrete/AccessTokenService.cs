using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Oauth.Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Oauth.Shared.Settings;
using Oauth.Shared.DTO;

namespace Oauth.Service.Concrete
{
    public class AccessTokenService : IAccessTokenService
    {
        private AppSettings _settings;
        private IUserService _userService;

        public AccessTokenService(IOptions<AppSettings> settings, IUserService userRepository)
        {
            _settings = settings.Value;
            _userService = userRepository;
        }

        public async Task<(string message, HttpStatusCode statusCode)> GetAccessToken(TokenRequest model)
        {
            var user = await _userService.FindByEmail(model.username);

            if (user != null && (_userService.VerifyPassword(model.password, user.Password)))
            {
                return (GetJwt(user.UserId.ToString()), HttpStatusCode.OK);
            }

            return ("User not found", HttpStatusCode.NotFound);
        }

        private string GetJwt(string client_id)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, client_id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var symmetricKeyAsBase64 = _settings.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new AccessTokenDTO
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)TimeSpan.FromMinutes(20).TotalSeconds
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

    }
}
