using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface IUserRepositories
    {
        Task<User?> GetByIdWithRolesAsync(Guid userId);
        Task<bool> IsEmailTakenAsync(string email, Guid excludeUserId);
        Task SaveChangesAsync();
    }

}
