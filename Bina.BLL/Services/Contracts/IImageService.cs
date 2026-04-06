using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Bina.BLL.DTOs.Common;

namespace Bina.BLL.Services.Contracts
{
    public interface IImageService
    {
        Task<ApiResponse<string>> UploadAsync(IFormFile file, int propertyId);
        Task<ApiResponse<bool>> DeleteAsync(int imageId, int userId);
        Task<ApiResponse<bool>> SetMainAsync(int imageId, int propertyId);
    }
}