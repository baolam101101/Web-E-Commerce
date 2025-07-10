using Web_E_Commerce.DTOs.Product.Requests;
using Web_E_Commerce.DTOs.Product.Responses;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductCreateResponse>> GetAllAsync(int page, int pageSize);
        Task<ProductCreateResponse?> GetByIdAsync(int id);
        Task<ProductCreateResponse> CreateAsync(ProductCreateRequest request);
        Task<ProductUpdateResponse?> UpdateAsync(int id, ProductUpdateRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
