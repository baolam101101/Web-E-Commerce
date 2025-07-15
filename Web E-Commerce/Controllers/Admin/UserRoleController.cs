using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.UserRoles.Requests;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/user-roles")]
    [Authorize(Roles = "Admin")]
    public class UserRoleController(AppDbContext context, IUserRoleService userRoleService) : ControllerBase
    {
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            try
            {
                var result = await userRoleService.AssignRoleAsync(request);
                return Ok(new
                {
                    message = "Role assigned successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await context.Roles
                .Select(r => r.Name)
                .ToListAsync();

            return Ok(new
            {
                message = "Get all roles successfully",
                data = roles
            });
        }

        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUserWithRoles()
        {
            var user = await userRoleService.GetAllUserWithRolesAsync();
            return Ok(new
            {
                message = "Get all users with their roles successfully",
                data = user
            });
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleRequest request)
        {
            try
            {
                var success = await userRoleService.RemoveRoleAsync(request);
                return Ok(new { message = "Role removed successfully", success });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
