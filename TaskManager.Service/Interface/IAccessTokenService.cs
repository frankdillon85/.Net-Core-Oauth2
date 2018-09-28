using System.Net;
using System.Threading.Tasks;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Service.Interface
{
    public interface IAccessTokenService
    {
        Task<(string message, HttpStatusCode statusCode)> GetAccessToken(TokenRequest model);
    }
}
