using Microsoft.AspNetCore.Mvc;
using Bina.BLL.Services.Contracts;
using Bina.BLL.DTOs.Common;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Bina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        /// <summary>
        /// Uploads an image explicitly to the wwwroot static content directory linked locally to property
        /// </summary>
        [HttpPost("upload")]
        public async Task<ActionResult<ApiResponse<string>>> UploadAsync(IFormFile file, [FromForm] int propertyId)
        {
            if (file == null || file.Length == 0) return BadRequest("Fayl seçilm?yib.");

            var response = await _imageService.UploadAsync(file, propertyId);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Explicit hard destruction removing it physically from disk. 
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAsync(int id)
        {
            var userId = GetCurrentUserId();
            var response = await _imageService.DeleteAsync(id, userId);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Overrides standard main flags within property mapping configuration
        /// </summary>
        [HttpPut("{id:int}/set-main")]
        public async Task<ActionResult<ApiResponse<bool>>> SetMainAsync(int id, [FromQuery] int propertyId)
        {
            var response = await _imageService.SetMainAsync(id, propertyId);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}