using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface IProductRepositories
    {
        // CRUD operations
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);

        // Filter
        Task<(IEnumerable<Product> Items, int TotalCount)> FilterAsync(
            int? categoryId,
            string? keyword,
            decimal? minPrice,
            decimal? maxPrice,
            string? sortBy,
            int page,
            int pageSize);
    }
}