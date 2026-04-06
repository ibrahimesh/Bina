using Bina.DAL.Models;
using Bina.DAL.Data;
using Bina.DAL.Repositories.Interfaces;
using Bina.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bina.DAL.Repositories.Implementations
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(BinaDbContext context) : base(context) { }

        public IQueryable<Property> GetBaseQuery()
        {
            return _context.Properties
                .Where(p => p.Status == PropertyStatus.Active)
                .Include(p => p.Images)
                .Include(p => p.City)
                .Include(p => p.District)
                .Include(p => p.Metro)
                .Include(p => p.Category)
                .AsSplitQuery()
                .AsNoTracking();
        }
    }
}