using Bina.DAL.Models;
using Bina.DAL.Data;
using Bina.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bina.DAL.Repositories.Implementations
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(BinaDbContext context) : base(context) { }

        public async Task<HashSet<int>> GetUserFavoriteIdsAsync(int userId)
        {
            var favoriteIds = await _dbSet
                .Where(f => f.UserId == userId)
                .Select(f => f.PropertyId)
                .ToListAsync();

            return new HashSet<int>(favoriteIds);
        }
    }
}