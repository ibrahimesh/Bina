using System.Collections.Generic;
using System.Threading.Tasks;
using Bina.BLL.DTOs.Property;
using Bina.BLL.DTOs.Common;

namespace Bina.BLL.Services.Contracts
{
    public interface IPropertyService
    {
        Task<ApiResponse<PropertyResponseDto>> CreateAsync(CreatePropertyDto dto, int userId);
        Task<ApiResponse<PropertyResponseDto>> GetByIdAsync(int id, int? currentUserId = null);
        Task<ApiResponse<PagedResult<PropertyListDto>>> SearchAsync(PropertyFilterDto filter, int? currentUserId = null);
        Task<ApiResponse<PropertyResponseDto>> UpdateAsync(int id, UpdatePropertyDto dto, int userId);
        Task<ApiResponse<bool>> DeleteAsync(int id, int userId);
        Task<ApiResponse<IEnumerable<PropertyListDto>>> GetByUserAsync(int userId);
    }
}