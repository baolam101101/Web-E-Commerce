using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Repositories.Implementations
{
    public class RoleRepositories(AppDbContext context) : IRoleRepositories
    {
        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await context.Roles
                .FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
