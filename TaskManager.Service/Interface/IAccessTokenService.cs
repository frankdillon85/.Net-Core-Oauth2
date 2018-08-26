using System;
using System.Net;
using System.Threading.Tasks;
using TaskManager.Data.Models.ViewModels;

namespace TaskManager.Service.Interface
{
    public interface IAccessTokenService
    {
        Task<(string message, HttpStatusCode statusCode)> GetAccessToken(TokenRequest model);
    }
}
