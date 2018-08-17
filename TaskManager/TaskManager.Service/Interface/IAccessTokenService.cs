using System;
using System.Net;
using System.Threading.Tasks;
using TaskManager.Data.Models.ViewModels;

namespace TaskManager.Service.Interface
{
    public interface IAccessTokenService
    {
        Task<Tuple<string, HttpStatusCode>> GetAccessToken(TokenRequest model);
    }
}
