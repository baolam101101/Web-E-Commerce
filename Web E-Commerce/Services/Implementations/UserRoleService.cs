using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.UserRoles.Requests;
using Web_E_Commerce.DTOs.Admin.UserRoles.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
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
                    MessageKeys.USER_NOT_FOUND, 
                    MessageDescriptions.USER_NOT_FOUND);

            var role = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == request.RoleName)
                ?? throw new RoleException(
                    MessageKeys.ROLE_NOT_FOUND,
                    MessageDescriptions.ROLE_NOT_FOUND);

            if (user.UserRoles.Any(ur => ur.RoleId == role.Id))
                throw new BadRequestException(
                    MessageKeys.USER_ALREADY_HAS_ROLE, 
                    $"User with ID {user.Id} already has the role '{role.Name}'.");

            user.UserRoles.Add(new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            await context.SaveChangesAsync();
            // Map the user to AssignRoleResponse
            var result = mapper.Map<AssignRoleResponse>(user);

            return ApiResponse<AssignRoleResponse>.Ok(result, MessageKeys.ROLE_ASSIGN_SUCCESS, MessageDescriptions.ROLE_ASSIGN_SUCCESS);
        }

        public async Task<ApiResponse<bool>> RemoveRoleAsync(RemoveRoleRequest request)
        {
            var user = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == request.UserId)
                ?? throw new NotFoundException(
                    MessageKeys.USER_NOT_FOUND,
                    $"No user with ID {request.UserId} exists in the system.");

            var role = user.UserRoles.FirstOrDefault(ur => ur.Role.Name == request.RoleName)
                ?? throw new BadRequestException(
                    MessageKeys.USER_DOES_NOT_HAVE_ROLE,
                    $"User with ID {user.Id} does not have the role '{request.RoleName}' to remove.");

            user.UserRoles.Remove(role);
            await context.SaveChangesAsync();

            return ApiResponse<bool>.Ok(true, MessageKeys.ROLE_REMOVE_SUCCESS, MessageDescriptions.ROLE_REMOVE_SUCCESS);
        }

        public async Task<ApiResponse<List<UserWithRolesResponse>>> GetAllUserWithRolesAsync()
        {
            var users = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            // Map the users to UserWithRolesResponse
            var result = mapper.Map<List<UserWithRolesResponse>>(users);

            return ApiResponse<List<UserWithRolesResponse>>.Ok(result, MessageKeys.GET_ALL_USERS_WITH_ROLES_SUCCESS, MessageDescriptions.GET_ALL_USERS_WITH_ROLES_SUCCESS);
        }

        public async Task<ApiResponse<UserWithRolesResponse>> GetUserRolesAsync(int userId)
        {
            var user = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new NotFoundException(
                    MessageKeys.USER_NOT_FOUND,
                    $"The user with ID {userId} does not exist in the system.");

            // Map the user to UserWithRolesResponse
            var result = mapper.Map<UserWithRolesResponse>(user);

            return ApiResponse<UserWithRolesResponse>.Ok(result, MessageKeys.GET_USER_ROLES_SUCCESS, MessageDescriptions.GET_USER_ROLES_SUCCESS);
        }

        public async Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync()
        {
            var roles = await context.Roles.ToListAsync();
            var roleDtos = mapper.Map<List<RoleDto>>(roles);

            return ApiResponse<List<RoleDto>>.Ok(roleDtos, MessageKeys.GET_ALL_ROLES_SUCCESS, MessageDescriptions.GET_ALL_ROLES_SUCCESS);
        }
    }
}
