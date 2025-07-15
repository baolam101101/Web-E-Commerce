using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.Data;
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
            if (await authService.UserExists(authRequest.UserName))
                return BadRequest("Username already exists");

            var user = await authService.Register(authRequest.UserName, authRequest.Password);

            return Ok(new
            {
                message = "You have successfully registered!",
                data = new { user.UserName }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {
            var accessToken = await authService.Login(authRequest.UserName, authRequest.Password);
            if (accessToken == null)
                return Unauthorized();

            return Ok(new AuthResponse
            {
                Message = "User login successfully!",
                Data = new AuthData { AccessToken = accessToken }
            });
        }
    }
}
