using Web_E_Commerce.DTOs.Admin.SellerRequest.Responses;
using Web_E_Commerce.DTOs.Seller.Requests;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface ISellerRequestService
    {
        public Task<ApiResponse<SellerRequestResponse>> RequestSellerAsync(SellerRequestDto dto);
    }
}