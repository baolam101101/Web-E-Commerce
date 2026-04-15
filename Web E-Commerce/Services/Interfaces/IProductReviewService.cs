using Web_E_Commerce.DTOs.ProductPreview.Requests;
using Web_E_Commerce.DTOs.ProductPreview.Responses;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IProductReviewService
    {
        Task<ApiResponse<ReviewSummaryResponse>> GetByProductIdAsync(Guid productId);
        Task<ApiResponse<ReviewResponse>> CreateAsync(Guid productId, CreateReviewRequest request);
        Task<ApiResponse<ReviewResponse>> UpdateAsync(Guid reviewId, UpdateReviewRequest request);
        Task<ApiResponse<bool>> DeleteAsync(Guid reviewId);
    }
}