using System.Threading.Tasks;
using Oauth.Data.DB.Models;

namespace Oauth.Data.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
