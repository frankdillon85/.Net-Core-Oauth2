using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Oauth.Data.DB.Models;
using Oauth.Data.Interface;

namespace Oauth.Data.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        JPVDBContext _context;
        public UserRepository(JPVDBContext context) : base(context){
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
