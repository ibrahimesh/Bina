using Bina.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bina.DAL.Repositories.Interfaces
{
    public interface IFavoriteRepository : IRepository<Favorite> 
    { 
        Task<HashSet<int>> GetUserFavoriteIdsAsync(int userId);
    }
}