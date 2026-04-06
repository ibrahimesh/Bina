using Microsoft.AspNetCore.Mvc;
using Bina.BLL.Services.Contracts;
using Bina.BLL.DTOs.Property;
using Bina.BLL.DTOs.Common;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Bina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
                return userId;
            return null;
        }

        /// <summary>
        /// A filterable, sortable, paginated property search endpoint over active properties
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PagedResult<PropertyListDto>>>> SearchAsync([FromQuery] PropertyFilterDto filter)
        {
            var response = await _propertyService.SearchAsync(filter, GetCurrentUserId());
            return Ok(response);
        }

        /// <summary>
        /// Retrieves full details for a single property by its ID, incrementing its view logic
        /// </summary>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<PropertyResponseDto>>> GetByIdAsync(int id)
        {
            var response = await _propertyService.GetByIdAsync(id, GetCurrentUserId());
            return response.Success ? Ok(response) : NotFound(response);
        }

        /// <summary>
        /// Creates a new property listing under the authorized user account
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ApiResponse<PropertyResponseDto>>> CreateAsync([FromBody] CreatePropertyDto dto)
        {
            var userId = GetCurrentUserId().Value;
            var response = await _propertyService.CreateAsync(dto, userId);
            return StatusCode(201, response);
        }

        /// <summary>
        /// Update an existing property owned by the user
        /// </summary>
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<PropertyResponseDto>>> UpdateAsync(int id, [FromBody] UpdatePropertyDto dto)
        {
            var userId = GetCurrentUserId().Value;
            var response = await _propertyService.UpdateAsync(id, dto, userId);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Sets a property owned by the user to 'Deleted' Status soft delete
        /// </summary>
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAsync(int id)
        {
            var userId = GetCurrentUserId().Value;
            var response = await _propertyService.DeleteAsync(id, userId);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Retrieves all listings of the authorized user regardless of status
        /// </summary>
        [HttpGet("my")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<IEnumerable<PropertyListDto>>>> GetByUserAsync()
        {
            var userId = GetCurrentUserId().Value;
            var response = await _propertyService.GetByUserAsync(userId);
            return Ok(response);
        }
    }
}