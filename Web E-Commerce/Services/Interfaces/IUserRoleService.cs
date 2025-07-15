using Web_E_Commerce.DTOs.Admin.UserRoles.Requests;
using Web_E_Commerce.DTOs.Admin.UserRoles.Responses;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<AssignRoleResponse> AssignRoleAsync(AssignRoleRequest request);
        Task<bool> RemoveRoleAsync(RemoveRoleRequest request);
        Task<List<UserWithRolesResponse>> GetAllUserWithRolesAsync(); 
    }
}
