using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.UserRoles.Requests;
using Web_E_Commerce.DTOs.Admin.UserRoles.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class UserRoleService(AppDbContext context, IMapper mapper) : IUserRoleService
    {
        public async Task<ApiResponse<AssignRoleResponse>> AssignRoleAsync(AssignRoleRequest request)
        {
            var user = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == request.UserId)
                ?? throw new NotFoundException(
                    "User not found", 
                    $"No user with ID {request.UserId} exists in the system.");

            var role = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == request.RoleName)
                ?? throw new RoleException(
                    "Role not found",
                    $"Role '{request.RoleName}' does not exist in the system.");

            if (user.UserRoles.Any(ur => ur.RoleId == role.Id))
                throw new BadRequestException(
                    "User already has this role", 
                    $"User with ID {user.Id} already has the role '{role.Name}'.");

            user.UserRoles.Add(new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            await context.SaveChangesAsync();
            // Map the user to AssignRoleResponse
            var result = mapper.Map<AssignRoleResponse>(user);

            return new ApiResponse<AssignRoleResponse>("Role assigned successfully", result);
        }

        public async Task<ApiResponse<bool>> RemoveRoleAsync(RemoveRoleRequest request)
        {
            var user = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == request.UserId)
                ?? throw new NotFoundException(
                    "User not found",
                    $"No user with ID {request.UserId} exists in the system.");

            var role = user.UserRoles.FirstOrDefault(ur => ur.Role.Name == request.RoleName)
                ?? throw new BadRequestException(
                    "User does not have this role",
                    $"User with ID {user.Id} does not have the role '{request.RoleName}' to remove.");

            user.UserRoles.Remove(role);
            await context.SaveChangesAsync();

            return new ApiResponse<bool>("Role removed successfully", true);
        }

        public async Task<ApiResponse<List<UserWithRolesResponse>>> GetAllUserWithRolesAsync()
        {
            var users = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            // Map the users to UserWithRolesResponse
            var result = mapper.Map<List<UserWithRolesResponse>>(users);

            return new ApiResponse<List<UserWithRolesResponse>>("Get all users with roles successfully", result);
        }

        public async Task<ApiResponse<UserWithRolesResponse>> GetUserRolesAsync(int userId)
        {
            var user = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new NotFoundException(
                    "User not found",
                    $"The user with ID {userId} does not exist in the system.");

            // Map the user to UserWithRolesResponse
            var result = mapper.Map<UserWithRolesResponse>(user);

            return new ApiResponse<UserWithRolesResponse>("Get user roles successfully", result);
        }

        public async Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync()
        {
            var roles = await context.Roles.ToListAsync();
            var roleDtos = mapper.Map<List<RoleDto>>(roles);

            return new ApiResponse<List<RoleDto>>("Get all roles successfully", roleDtos);
        }
    }
}
