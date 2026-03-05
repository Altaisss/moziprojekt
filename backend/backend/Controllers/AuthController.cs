using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] FelhasznaloRequest dto)
        {
            var (success, message) = await _authService.RegisterAsync(dto);
            return success ? Ok(message) : Conflict(message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var (success, token, message) = await _authService.LoginAsync(dto);
            return success ? Ok(new { token }) : Unauthorized(message);
        }
    }
}
