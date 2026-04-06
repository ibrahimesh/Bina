using System.Threading.Tasks;
using Bina.BLL.DTOs.User;
using Bina.BLL.DTOs.Common;

namespace Bina.BLL.Services.Contracts
{
    public interface IUserService
    {
        Task<ApiResponse<UserProfileDto>> GetProfileAsync(int userId);
        Task<ApiResponse<UserProfileDto>> UpdateProfileAsync(int userId, UpdateProfileDto dto);
    }
}