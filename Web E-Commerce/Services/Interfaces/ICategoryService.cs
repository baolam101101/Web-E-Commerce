using Web_E_Commerce.DTOs.Category.Requests;
using Web_E_Commerce.DTOs.Category.Responses;

namespace Web_E_Commerce.Services.Interfaces
{

    public interface ICategoryService
    {
        Task<CategoryResponse> GetByIdAsync(int id);
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse> CreateAsync(CategoryCreateRequest request);
        Task<CategoryResponse> UpdateAsync(int id, CategoryUpdateRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
