using System.Threading.Tasks;
using Bina.BLL.DTOs.User;
using Bina.BLL.DTOs.Common;

namespace Bina.BLL.Services.Contracts
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterDto dto);
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto dto);
    }
}