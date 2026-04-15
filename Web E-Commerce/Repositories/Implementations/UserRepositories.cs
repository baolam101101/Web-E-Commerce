using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Repositories.Implementations
{
    public class UserRepositories(AppDbContext context) : IUserRepositories
    {
        public async Task<User?> GetByIdWithRolesAsync(Guid userId)
        {
            return await context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsEmailTakenAsync(string email, Guid excludeUserId)
        {
            return await context.Users
                .AnyAsync(u =>
                    u.Email == email &&
                    u.Id != excludeUserId);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
