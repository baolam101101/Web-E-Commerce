using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface IProductReviewRepositories
    {
        Task<List<ProductReview>> GetByProductIdAsync(Guid productId);
        Task<ProductReview?> GetByIdAsync(Guid reviewId);
        Task<ProductReview?> GetByUserAndProductAsync(Guid userId, Guid productId);
        Task<ProductReview> CreateAsync(ProductReview review);
        Task UpdateAsync(ProductReview review);
        Task DeleteAsync(ProductReview review);
        Task<bool> HasUserPurchasedProductAsync(Guid userId, Guid productId);
    }
}