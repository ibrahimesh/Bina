using System.Collections.Generic;
using System.Threading.Tasks;
using Bina.BLL.DTOs.Property;
using Bina.BLL.DTOs.Common;

namespace Bina.BLL.Services.Contracts
{
    public interface IFavoriteService
    {
        Task<ApiResponse<bool>> ToggleAsync(int userId, int propertyId);
        Task<ApiResponse<IEnumerable<PropertyListDto>>> GetUserFavoritesAsync(int userId);
    }
}