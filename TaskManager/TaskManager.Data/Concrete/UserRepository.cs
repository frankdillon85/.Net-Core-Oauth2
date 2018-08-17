using TaskManager.Data.DB.Models;
using TaskManager.Data.Interface;

namespace TaskManager.Data.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(JPVDBContext context) : base(context){}
    }
}
