using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface IProductRepositories
    {
        // CRUD operations
        IQueryable<Product> GetQueryable();
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product> CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);
        Task<bool> ExistsAsync(string name, Guid categoryId);
        Task<bool> SlugExistsAsync(string slug);
        Task<Product?> GetBySlugAsync(string slug);
        Task<PagedResult<Product>> GetProductsAsync(ProductFilterDto filter);
        Task<List<Product>> GetRelatedProductsAsync(
            Guid categoryId,
            Guid excludeProductId
        );
        Task IncrementViewAsync(string slug);
        Task<List<Product>> GetByIdsAsync(List<Guid> ids);
    }
}
