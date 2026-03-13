using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Auth.Requests;
using Web_E_Commerce.DTOs.Auth.Responses;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthRequest authRequest)
        {
            var response = await authService.Register(authRequest);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {
            var response = await authService.Login(authRequest);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var response = await authService.RefreshTokenAsync(request.RefreshToken);
            return Ok(response);
        }
    }
}