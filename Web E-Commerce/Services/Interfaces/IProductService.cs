using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Client.Product.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Models;


namespace Web_E_Commerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<PagedResult<ProductResponse>>> GetProductsAsync(ProductFilterDto filter);
        Task<ApiResponse<ProductResponse>> GetByIdAsync(int id);
        Task<ApiResponse<ProductResponse>> GetBySlugAsync(string slug);
        Task<ApiResponse<List<ProductResponse>>> GetRelatedProductsAsync(string slug);
        Task<ApiResponse<ProductResponse>> CreateAsync(ProductCreateRequest request);
        Task<ApiResponse<ProductResponse>> UpdateAsync(int id, ProductUpdateRequest request);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        Task<ApiResponse<int>> IncrementViewAsync(string slug);
    }
}