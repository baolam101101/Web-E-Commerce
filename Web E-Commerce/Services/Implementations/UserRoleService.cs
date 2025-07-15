using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.UserRoles.Requests;
using Web_E_Commerce.DTOs.Admin.UserRoles.Responses;
using Web_E_Commerce.Models;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class UserRoleService(AppDbContext context) : IUserRoleService
    {
        public async Task<AssignRoleResponse> AssignRoleAsync(AssignRoleRequest request)
        {
            var user = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == request.UserId) ?? throw new Exception("User not found");

            var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == request.RoleName) ?? throw new Exception("Role not found");

            if (user.UserRoles.Any(ur => ur.RoleId == role.Id)) throw new Exception("User already has this role");

            user.UserRoles.Add(new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            await context.SaveChangesAsync();

            return new AssignRoleResponse
            {
                UserId = user.Id,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
            };
        }

        public async Task<bool> RemoveRoleAsync(RemoveRoleRequest request)
        {
            var user = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == request.UserId) ?? throw new Exception("User not found");

            var role = user.UserRoles.FirstOrDefault(ur => ur.Role.Name == request.RoleName) ?? throw new Exception("User does not have this role");

            user.UserRoles.Remove(role);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserWithRolesResponse>> GetAllUserWithRolesAsync()
        {
            return await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new UserWithRolesResponse
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList()
                })
                .ToListAsync();
        }
    }
}
