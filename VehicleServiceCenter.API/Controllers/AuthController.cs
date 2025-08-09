using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleServiceCenter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceCenter.Application.DTOs.Auth;
using VehicleServiceCenter.Application.Interfaces;

namespace VehicleServiceCenter.Presentation.Controllers
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
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}




