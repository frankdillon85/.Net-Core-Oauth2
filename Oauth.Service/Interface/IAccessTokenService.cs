using System.Net;
using System.Threading.Tasks;
using Oauth.Shared.DTO;

namespace Oauth.Service.Interface
{
    public interface IAccessTokenService
    {
        Task<(string message, HttpStatusCode statusCode)> GetAccessToken(TokenRequest model);
    }
}
