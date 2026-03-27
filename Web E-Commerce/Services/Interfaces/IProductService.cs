using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Client.Product.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Models;


namespace Web_E_Commerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<PagedResult<ProductResponse>>> GetProductsAsync(ProductFilterDto filter);
        Task<ApiResponse<ProductResponse>> GetByIdAsync(Guid id);
        Task<ApiResponse<ProductResponse>> GetBySlugAsync(string slug);
        Task<ApiResponse<List<ProductResponse>>> GetRelatedProductsAsync(string slug);
        Task<ApiResponse<ProductResponse>> CreateAsync(ProductCreateRequest request);
        Task<ApiResponse<ProductResponse>> UpdateAsync(Guid id, ProductUpdateRequest request);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
        Task<ApiResponse<int>> IncrementViewAsync(string slug);
    }
}