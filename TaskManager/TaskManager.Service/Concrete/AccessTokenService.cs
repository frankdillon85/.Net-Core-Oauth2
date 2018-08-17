using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Interface;
using System.Net;
using TaskManager.Service.Interface;
using TaskManager.Data.Models.Settings;
using TaskManager.Data.Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace TaskManager.Service.Concrete
{
    public class AccessTokenService: IAccessTokenService
    {
        private AppSettings _settings;
        private IUserService _userRepository;

        public AccessTokenService(IOptions<AppSettings> settings, IUserService userRepository)
        {
            _settings = settings.Value;
            _userRepository = userRepository;
        }

        public async Task<Tuple<string, HttpStatusCode>> GetAccessToken(TokenRequest model)
        {
            var user = (await _userRepository.FindWhere(x => x.Email == model.username)).FirstOrDefault();

            if (user != null)
            {
                try
                {
                    return  Tuple.Create(GetJwt(user.UserId.ToString()), HttpStatusCode.OK);
                }
                catch (Exception)
                {
                    return Tuple.Create("Internal Server Error", HttpStatusCode.InternalServerError);
                }
            }
           
            return Tuple.Create("User not found", HttpStatusCode.NotFound);
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

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(20).TotalSeconds
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

    }
}
