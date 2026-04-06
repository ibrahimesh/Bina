using Bina.BLL.DTOs.User;
using Bina.BLL.DTOs.Common;
using Bina.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user account with validated credentials mapping to default user role roles
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RegisterAsync([FromBody] RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return response.Success ? StatusCode(201, response) : BadRequest(response);
        }

        /// <summary>
        /// Validates Login credentials directly against DB hashing to return fully formed JWT payloads
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> LoginAsync([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return response.Success ? Ok(response) : Unauthorized(response);
        }
    }
}