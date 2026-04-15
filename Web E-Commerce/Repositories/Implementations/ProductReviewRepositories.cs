using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Repositories.Implementations
{
    public class ProductReviewRepositories(AppDbContext context) : IProductReviewRepositories
    {
        public async Task<List<ProductReview>> GetByProductIdAsync(Guid productId)
        {
            return await context.ProductReviews
                .Include(r => r.User)
                .Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<ProductReview?> GetByIdAsync(Guid reviewId)
        {
            return await context.ProductReviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public async Task<ProductReview?> GetByUserAndProductAsync(Guid userId, Guid productId)
        {
            return await context.ProductReviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == productId);
        }

        public async Task<ProductReview> CreateAsync(ProductReview review)
        {
            context.ProductReviews.Add(review);
            await context.SaveChangesAsync();
            return review;
        }

        public async Task UpdateAsync(ProductReview review)
        {
            context.ProductReviews.Update(review);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductReview review)
        {
            context.ProductReviews.Remove(review);
            await context.SaveChangesAsync();
        }

        // Chỉ cho phép review nếu đã mua và order đã Completed
        public async Task<bool> HasUserPurchasedProductAsync(Guid userId, Guid productId)
        {
            return await context.Orders
                .Where(o =>
                    o.UserId == userId &&
                    o.OrderStatus == OrderStatus.Completed)
                .AnyAsync(o => o.OrderItems.Any(oi => oi.ProductId == productId));
        }
    }
}