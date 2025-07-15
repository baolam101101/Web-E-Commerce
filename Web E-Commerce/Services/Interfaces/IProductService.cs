using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Client.Product.Responses;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<PaginationWrapper<ProductCreateResponse>> GetAllAsync(int page, int pageSize);
        Task<ProductCreateResponse?> GetByIdAsync(int id);
        Task<ProductCreateResponse> CreateAsync(ProductCreateRequest request);
        Task<ProductUpdateResponse?> UpdateAsync(int id, ProductUpdateRequest request);
        Task<bool> DeleteAsync(int id);
    }
}