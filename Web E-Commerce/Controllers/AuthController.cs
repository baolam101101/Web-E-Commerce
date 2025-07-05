using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Auth.Requests;
using Web_E_Commerce.DTOs.Auth.Responses;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthRequest authRequest)
        {
            if (await _authService.UserExists(authRequest.UserName))
                return BadRequest("Username already exists");

            // Check role valid
            if (!Enum.TryParse<UserRole>(authRequest.Role, ignoreCase: true, out var parsedRole))
            {
                var allowedRoles = string.Join(", ", Enum.GetNames(typeof(UserRole)));
                return BadRequest($"Invalid role. Allowed roles: {allowedRoles}");
            }

            var user = await _authService.Register(authRequest.UserName, authRequest.Password, parsedRole);
            return Ok(new
            {
                message = "You have successfully registered!",
                data = new { user.UserName, user.Role }
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {
            var accessToken = await _authService.Login(authRequest.UserName, authRequest.Password);
            if (accessToken == null)
                return Unauthorized();

            return Ok(new AuthResponse
            {
                Message = "User login successfully!",
                Data = new AuthData { AccessToken = accessToken }
            });
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            var roles = Enum.GetNames(typeof(UserRole));
            return Ok(new
            {
                message = "Get all roles successfully",
                data = new { roles }
            });
        }
    }
}
