using Bina.DAL.Models;
using Bina.DAL.Data;
using Bina.DAL.Repositories.Interfaces;
namespace Bina.DAL.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BinaDbContext context) : base(context) { }
    }
}