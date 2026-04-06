using Microsoft.AspNetCore.Mvc;
using Bina.BLL.Services.Contracts;
using Bina.BLL.DTOs.Common;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Bina.BLL.DTOs.Property;

namespace Bina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        /// <summary>
        /// Registers or Removes an active favorite linked dynamically via relational mappings 
        /// </summary>
        [HttpPost("{propertyId:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> ToggleAsync(int propertyId)
        {
            var userId = GetCurrentUserId();
            var response = await _favoriteService.ToggleAsync(userId, propertyId);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Collects all properties currently preserved as 'favorites' for the requester
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PropertyListDto>>>> GetUserFavoritesAsync()
        {
            var userId = GetCurrentUserId();
            var response = await _favoriteService.GetUserFavoritesAsync(userId);
            return Ok(response);
        }
    }
}