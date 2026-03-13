using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface ICategoryRepositories
    {
        Task<IEnumerable<Category>> GetAllAsync(); // Admin
        Task<IEnumerable<Category>> GetAllActiveAsync(); // Client 
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> ExistsAsync(string normalizedName);
    }
}
