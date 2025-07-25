using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Client.Product.Responses;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<PaginationWrapper<ProductResponse>>> GetAllAsync(int page, int pageSize);
        Task<ApiResponse<ProductResponse?>> GetByIdAsync(int id);
        Task<ApiResponse<ProductResponse>> CreateAsync(ProductCreateRequest request);
        Task<ApiResponse<ProductResponse?>> UpdateAsync(int id, ProductUpdateRequest request);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}