using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.UserRoles.Requests;
using Web_E_Commerce.DTOs.Admin.UserRoles.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/user-roles")]
    [Authorize(Roles = "Admin")]
    public class UserRoleController(IUserRoleService userRoleService) : ControllerBase
    {
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var result = await userRoleService.AssignRoleAsync(request);
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await userRoleService.GetAllRolesAsync();
            return Ok(response);
        }

        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUserWithRoles()
        {
            var user = await userRoleService.GetAllUserWithRolesAsync();
            return Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            var result = await userRoleService.GetUserRolesAsync(userId);
            return Ok(result);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleRequest request)
        {
            var result = await userRoleService.RemoveRoleAsync(request);
            return Ok(result);
        }
    }
}
