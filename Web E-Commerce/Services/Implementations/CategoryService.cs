using AutoMapper;
using Azure.Core;
using Web_E_Commerce.DTOs.Category.Requests;
using Web_E_Commerce.DTOs.Category.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Implementations;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;
using Web_E_Commerce.Utilities;

namespace Web_E_Commerce.Services.Implementations
{
    public class CategoryService(ICategoryRepositories categoryRepositories,
        IMapper mapper) : ICategoryService
    {
        public async Task<ApiResponse<PaginationWrapper<CategoryResponse>>> GetAllAsync(int page, int pageSize, bool includeInactive)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var categories = includeInactive
                ? await categoryRepositories.GetAllAsync() // Admin
                : await categoryRepositories.GetAllActiveAsync(); // Client

            var totalItems = categories.Count();

            var pageCategories = categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mapped = mapper.Map<IEnumerable<CategoryResponse>>(pageCategories);

            var pagination = new PaginationWrapper<CategoryResponse>(
                page: page,
                pageSize: pageSize,
                totalItems,
                items: mapped
            );

            return ApiResponse<PaginationWrapper<CategoryResponse>>.Ok(
                pagination,
                MessageKeys.GET_ALL_CATEGORIES_SUCCESS,
                MessageDescriptions.GET_ALL_CATEGORIES_SUCCESS
            );
        }

        // =========================================
        // GET CATEGORY BY ID
        // =========================================
        public async Task<ApiResponse<CategoryResponse>> GetByIdAsync(Guid id)
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
            // category Name
            var name = dto.Name.Trim();
            // normalize name
            var normalizedName = name.ToLower();

            // check duplicate
            var exists = await categoryRepositories.ExistsAsync(normalizedName);

            if (exists)
                throw new BadRequestException(
                    MessageKeys.CATEGORY_ALREADY_EXISTS,
                    MessageDescriptions.CATEGORY_ALREADY_EXISTS
                );

            var slug = SlugHelper.Generate(name);

            var category = mapper.Map<Category>(dto);

            category.Name = name;
            category.NormalizedName = normalizedName;
            category.Slug = slug;

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
        public async Task<ApiResponse<CategoryResponse>> UpdateAsync(Guid id, CategoryUpdateRequest dto)
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
        // DEACTIVE CATEGORY
        // =========================================
        public async Task<ApiResponse<bool>> DeactivateAsync(Guid id)
        {
            var category = await categoryRepositories.GetByIdAsync(id);

            if (category == null)
            {
                return ApiResponse<bool>.Fail(
                    MessageKeys.CATEGORY_NOT_FOUND,
                    MessageDescriptions.CATEGORY_NOT_FOUND
                );
            }

            if (!category.IsActive)
            {
                return ApiResponse<bool>.Ok(
                    true,
                    MessageKeys.CATEGORY_ALREADY_INACTIVE,
                    MessageDescriptions.CATEGORY_ALREADY_INACTIVE
                );
            }

            category.IsActive = false;
            await categoryRepositories.UpdateAsync(category);

            return ApiResponse<bool>.Ok(
                true,
                MessageKeys.DEACTIVATE_CATEGORY_SUCCESS,
                MessageDescriptions.DEACTIVATE_CATEGORY_SUCCESS
            );
        }

        // =========================================
        // ACTIVE CATEGORY
        // =========================================
        public async Task<ApiResponse<bool>> ActivateAsync(Guid id)
        {
            var category = await categoryRepositories.GetByIdAsync(id);

            if (category == null)
            {
                return ApiResponse<bool>.Fail(
                    MessageKeys.CATEGORY_NOT_FOUND,
                    MessageDescriptions.CATEGORY_NOT_FOUND
                );
            }

            if (category.IsActive)
            {
                return ApiResponse<bool>.Ok(
                    true,
                    MessageKeys.CATEGORY_ALREADY_ACTIVE,
                    MessageDescriptions.CATEGORY_ALREADY_ACTIVE
                );
            }

            category.IsActive = true;
            await categoryRepositories.UpdateAsync(category);

            return ApiResponse<bool>.Ok(
                true,
                MessageKeys.ACTIVATE_CATEGORY_SUCCESS,
                MessageDescriptions.ACTIVATE_CATEGORY_SUCCESS
            );
        }


    }
}
