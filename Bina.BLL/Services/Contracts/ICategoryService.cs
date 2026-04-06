using Bina.BLL.DTOs.Common;
using Bina.BLL.DTOs.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bina.BLL.Services.Contracts
{
    public interface ICategoryService
    {
        Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllCategoriesAsync();
    }
}