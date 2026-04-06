using Bina.DAL.Models;
using Bina.DAL.Data;
using Bina.DAL.Repositories.Interfaces;
namespace Bina.DAL.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BinaDbContext context) : base(context) { }
    }
}