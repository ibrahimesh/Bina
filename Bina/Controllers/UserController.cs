using Microsoft.AspNetCore.Mvc;
using Bina.BLL.Services.Contracts;
using Bina.BLL.DTOs.Common;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Bina.BLL.DTOs.User;

namespace Bina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        /// <summary>
        /// Retrieves sensitive profile elements bound identically to caller permissions
        /// </summary>
        [HttpGet("profile")]
        public async Task<ActionResult<ApiResponse<UserProfileDto>>> GetProfileAsync()
        {
            var userId = GetCurrentUserId();
            var response = await _userService.GetProfileAsync(userId);
            return response.Success ? Ok(response) : NotFound(response);
        }

        /// <summary>
        /// Update Profile identifiers selectively (only explicitly supplied mappings) 
        /// </summary>
        [HttpPut("profile")]
        public async Task<ActionResult<ApiResponse<UserProfileDto>>> UpdateProfileAsync([FromBody] UpdateProfileDto dto)
        {
            var userId = GetCurrentUserId();
            var response = await _userService.UpdateProfileAsync(userId, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}