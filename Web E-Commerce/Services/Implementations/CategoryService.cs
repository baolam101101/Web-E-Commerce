using AutoMapper;
using Web_E_Commerce.DTOs.Category.Requests;
using Web_E_Commerce.DTOs.Category.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Implementations;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class CategoryService(ICategoryRepositories categoryRepositories,
        IMapper mapper) : ICategoryService
    {
        public async Task<ApiResponse<PaginationWrapper<CategoryResponse>>> GetAllAsync(int page, int pageSize)
        {
            var allCategory = await categoryRepositories.GetAllAsync();

            var pageCategories = allCategory
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mapped = mapper.Map<IEnumerable<CategoryResponse>>(pageCategories);

            var pagination = new PaginationWrapper<CategoryResponse>(
                items: mapped,
                page: page,
                pageSize: pageSize,
                totalItems: allCategory.Count());

            return ApiResponse<PaginationWrapper<CategoryResponse>>.Ok(
                pagination,
                MessageKeys.GET_ALL_CATEGORIES_SUCCESS,
                MessageDescriptions.GET_ALL_CATEGORIES_SUCCESS
            );
        }

        // =========================================
        // GET CATEGORY BY ID
        // =========================================
        public async Task<ApiResponse<CategoryResponse>> GetByIdAsync(int id)
        {
            var category = await categoryRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(MessageKeys.CATEGORY_NOT_FOUND, MessageDescriptions.CATEGORY_NOT_FOUND);

            var response = mapper.Map<CategoryResponse>(category);

            return ApiResponse<CategoryResponse>.Ok(
                response,
                MessageKeys.GET_CATEGORY_DETAILS_SUCCESS,
                MessageDescriptions.GET_CATEGORY_DETAILS_SUCCESS
            );
        }

        // =========================================
        // CREATE CATEGORY
        // =========================================
        public async Task<ApiResponse<CategoryResponse>> CreateAsync(CategoryCreateRequest dto)
        {
            var category = mapper.Map<Category>(dto);
            var created = await categoryRepositories.CreateAsync(category);
            var response = mapper.Map<CategoryResponse>(created);

            return ApiResponse<CategoryResponse>.Ok(
                response,
                MessageKeys.CREATE_CATEGORY_SUCCESS,
                MessageDescriptions.CREATE_CATEGORY_SUCCESS
            );
        }

        // =========================================
        // UPDATE CATEGORY
        // =========================================
        public async Task<ApiResponse<CategoryResponse>> UpdateAsync(int id, CategoryUpdateRequest dto)
        {
            var category = await categoryRepositories.GetByIdAsync(id);
            if (category == null)
            {
                return ApiResponse<CategoryResponse>.Fail(
                    MessageKeys.CATEGORY_NOT_FOUND,
                    MessageDescriptions.CATEGORY_NOT_FOUND
                );
            }

            category.Name = dto.Name;
            await categoryRepositories.UpdateAsync(category);

            var response = mapper.Map<CategoryResponse>(category);

            return ApiResponse<CategoryResponse>.Ok(
                response,
                MessageKeys.UPDATE_CATEGORY_SUCCESS,
                MessageDescriptions.UPDATE_CATEGORY_SUCCESS
            );
        }

        // =========================================
        // DELETE CATEGORY
        // =========================================
        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var category = await categoryRepositories.GetByIdAsync(id);
            if (category == null)
            {
                return ApiResponse<bool>.Fail(
                    MessageKeys.CATEGORY_NOT_FOUND,
                    MessageDescriptions.CATEGORY_NOT_FOUND
                );
            }

            await categoryRepositories.DeleteAsync(category);

            return ApiResponse<bool>.Ok(
                true,
                MessageKeys.DELETE_CATEGORY_SUCCESS,
                MessageDescriptions.DELETE_CATEGORY_SUCCESS
            );
        }
    }
}
