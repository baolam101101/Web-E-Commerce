using Web_E_Commerce.DTOs.Admin.UserRoles.Requests;
using Web_E_Commerce.DTOs.Admin.UserRoles.Responses;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<ApiResponse<AssignRoleResponse>> AssignRoleAsync(AssignRoleRequest request);
        Task<ApiResponse<bool>> RemoveRoleAsync(RemoveRoleRequest request);
        Task<ApiResponse<List<UserWithRolesResponse>>> GetAllUserWithRolesAsync();
        Task<ApiResponse<UserWithRolesResponse>> GetUserRolesAsync(int userId);
        Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync();
    }
}