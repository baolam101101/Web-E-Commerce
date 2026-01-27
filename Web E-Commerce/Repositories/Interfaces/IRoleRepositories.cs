using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface IRoleRepositories
    {
        Task<Role?> GetByNameAsync(string roleName);
    }
}
