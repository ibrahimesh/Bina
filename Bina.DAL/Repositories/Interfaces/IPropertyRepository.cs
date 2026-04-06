using Bina.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Bina.DAL.Repositories.Interfaces
{
    public interface IPropertyRepository : IRepository<Property> 
    {
        IQueryable<Property> GetBaseQuery();
    }
}