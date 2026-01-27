using Web_E_Commerce.DTOs.Admin.SellerRequest.Responses;
using Web_E_Commerce.DTOs.Seller.Requests;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface ISellerRequestService
    {
        public Task<ApiResponse<SellerRequestResponse>> RequestSellerAsync(SellerRequestDto dto);

        Task<ApiResponse<PaginationWrapper<SellerRequestResponse>>> GetAllAsync(
            SellerRequestStatus? status,
            int page,
            int pageSize);

        Task<ApiResponse<bool>> ApproveAsync(int requestId);

        Task<ApiResponse<bool>> RejectAsync(int requestId);
    }
}