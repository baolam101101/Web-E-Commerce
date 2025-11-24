using Web_E_Commerce.DTOs.Category.Requests;
using Web_E_Commerce.DTOs.Category.Responses;
using Web_E_Commerce.DTOs.Shared;

namespace Web_E_Commerce.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<PaginationWrapper<CategoryResponse>>> GetAllAsync(int page, int pageSize);
        Task<ApiResponse<CategoryResponse>> GetByIdAsync(int id);
        Task<ApiResponse<CategoryResponse>> CreateAsync(CategoryCreateRequest request);
        Task<ApiResponse<CategoryResponse>> UpdateAsync(int id, CategoryUpdateRequest request);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
