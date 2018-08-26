using System.Threading.Tasks;
using TaskManager.Data.DB.Models;

namespace TaskManager.Data.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
