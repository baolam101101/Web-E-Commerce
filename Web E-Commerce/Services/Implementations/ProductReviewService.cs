using AutoMapper;
using Web_E_Commerce.DTOs.ProductPreview.Requests;
using Web_E_Commerce.DTOs.ProductPreview.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class ProductReviewService(
        IProductReviewRepositories reviewRepositories,
        IProductRepositories productRepositories,
        ICurrentUserService currentUser,
        IMapper mapper) : IProductReviewService
    {
        public async Task<ApiResponse<ReviewSummaryResponse>> GetByProductIdAsync(Guid productId)
        {
            _ = await productRepositories.GetByIdAsync(productId)
                ?? throw new NotFoundException(
                    MessageKeys.PRODUCT_NOT_FOUND,
                    MessageDescriptions.PRODUCT_NOT_FOUND);

            var reviews = await reviewRepositories.GetByProductIdAsync(productId);

            var mapped = mapper.Map<List<ReviewResponse>>(reviews);

            var summary = new ReviewSummaryResponse
            {
                TotalReviews = mapped.Count,
                AverageRating = mapped.Count > 0
                    ? Math.Round(mapped.Average(r => r.Rating), 1)
                    : 0,
                Reviews = mapped
            };

            return ApiResponse<ReviewSummaryResponse>.Ok(
                summary,
                MessageKeys.GET_REVIEWS_SUCCESS,
                MessageDescriptions.GET_REVIEWS_SUCCESS);
        }

        public async Task<ApiResponse<ReviewResponse>> CreateAsync(
            Guid productId,
            CreateReviewRequest request)
        {
            // Validate rating range
            if (request.Rating < 1 || request.Rating > 5)
                throw new BadRequestException(
                    MessageKeys.INVALID_REVIEW_RATING,
                    MessageDescriptions.INVALID_REVIEW_RATING);

            var userId = currentUser.UserId;

            _ = await productRepositories.GetByIdAsync(productId)
                ?? throw new NotFoundException(
                    MessageKeys.PRODUCT_NOT_FOUND,
                    MessageDescriptions.PRODUCT_NOT_FOUND);

            // Phải mua và order đã Completed mới được review
            var hasPurchased = await reviewRepositories.HasUserPurchasedProductAsync(userId, productId);
            if (!hasPurchased)
                throw new BadRequestException(
                    MessageKeys.REVIEW_PURCHASE_REQUIRED,
                    MessageDescriptions.REVIEW_PURCHASE_REQUIRED);

            // Mỗi user chỉ review 1 lần / product
            var existing = await reviewRepositories.GetByUserAndProductAsync(userId, productId);
            if (existing != null)
                throw new BadRequestException(
                    MessageKeys.REVIEW_ALREADY_EXISTS,
                    MessageDescriptions.REVIEW_ALREADY_EXISTS);

            var review = new ProductReview
            {
                ProductId = productId,
                UserId = userId,
                Rating = request.Rating,
                Comment = request.Comment.Trim()
            };

            var created = await reviewRepositories.CreateAsync(review);

            // Reload để include User (cho mapper lấy UserName)
            var reloaded = await reviewRepositories.GetByIdAsync(created.Id)
                ?? created;

            var response = mapper.Map<ReviewResponse>(reloaded);

            return ApiResponse<ReviewResponse>.Ok(
                response,
                MessageKeys.CREATE_REVIEW_SUCCESS,
                MessageDescriptions.CREATE_REVIEW_SUCCESS);
        }

        public async Task<ApiResponse<ReviewResponse>> UpdateAsync(
            Guid reviewId,
            UpdateReviewRequest request)
        {
            if (request.Rating < 1 || request.Rating > 5)
                throw new BadRequestException(
                    MessageKeys.INVALID_REVIEW_RATING,
                    MessageDescriptions.INVALID_REVIEW_RATING);

            var userId = currentUser.UserId;

            var review = await reviewRepositories.GetByIdAsync(reviewId)
                ?? throw new NotFoundException(
                    MessageKeys.REVIEW_NOT_FOUND,
                    MessageDescriptions.REVIEW_NOT_FOUND);

            // Chỉ owner mới được sửa
            if (review.UserId != userId)
                throw new ForbiddenException(
                    MessageKeys.FORBIDDEN,
                    MessageDescriptions.FORBIDDEN);

            review.Rating = request.Rating;
            review.Comment = request.Comment.Trim();

            await reviewRepositories.UpdateAsync(review);

            var response = mapper.Map<ReviewResponse>(review);

            return ApiResponse<ReviewResponse>.Ok(
                response,
                MessageKeys.UPDATE_REVIEW_SUCCESS,
                MessageDescriptions.UPDATE_REVIEW_SUCCESS);
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid reviewId)
        {
            var userId = currentUser.UserId;

            var review = await reviewRepositories.GetByIdAsync(reviewId)
                ?? throw new NotFoundException(
                    MessageKeys.REVIEW_NOT_FOUND,
                    MessageDescriptions.REVIEW_NOT_FOUND);

            // Owner hoặc Admin đều được xóa — Admin check ở Controller level
            if (review.UserId != userId)
                throw new ForbiddenException(
                    MessageKeys.FORBIDDEN,
                    MessageDescriptions.FORBIDDEN);

            await reviewRepositories.DeleteAsync(review);

            return ApiResponse<bool>.Ok(
                true,
                MessageKeys.DELETE_REVIEW_SUCCESS,
                MessageDescriptions.DELETE_REVIEW_SUCCESS);
        }
    }
}