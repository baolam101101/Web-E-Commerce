using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Client.Product.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;
using Web_E_Commerce.Utilities;

namespace Web_E_Commerce.Services.Implementations
{
    public class ProductService(
        IProductRepositories productRepositories,
        ICategoryRepositories categoryRepositories,
        ICurrentUserService currentUserService,
        IMapper mapper) : IProductService
    {
        public async Task<ApiResponse<PagedResult<ProductResponse>>> GetProductsAsync(ProductFilterDto filter)
        {
            if (filter.Page <= 0 || filter.PageSize <= 0)
                throw new BadRequestException(
                    MessageKeys.INVALID_PAGINATION_PARAMETERS,
                    MessageDescriptions.INVALID_PAGINATION_PARAMETERS
                );

            var result = await productRepositories.GetProductsAsync(filter);

            var mapped = mapper.Map<List<ProductResponse>>(result.Items);

            var paged = new PagedResult<ProductResponse>(
                mapped,
                result.TotalCount,
                filter.Page,
                filter.PageSize
            );

            return ApiResponse<PagedResult<ProductResponse>>.Ok(
                paged,
                MessageKeys.GET_ALL_PRODUCTS_SUCCESS,
                MessageDescriptions.GET_ALL_PRODUCTS_SUCCESS
            );
        }
        public async Task<ApiResponse<List<ProductResponse>>> GetRelatedProductsAsync(string slug)
        {
            var product = await productRepositories.GetBySlugAsync(slug)
                ?? throw new NotFoundException(
                    MessageKeys.PRODUCT_NOT_FOUND,
                    MessageDescriptions.PRODUCT_NOT_FOUND
                );

            var related = await productRepositories.GetRelatedProductsAsync(
                product.CategoryId,
                product.Id
            );

            var response = mapper.Map<List<ProductResponse>>(related);

            return ApiResponse<List<ProductResponse>>.Ok(
                response,
                MessageKeys.GET_RELATED_PRODUCT_SUCCESS,
                MessageDescriptions.GET_RELATED_PRODUCT_SUCCESS
            );
        }

        public async Task<ApiResponse<ProductResponse>> GetByIdAsync(Guid id)
        {
            var product = await productRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(
                    MessageKeys.PRODUCT_NOT_FOUND, 
                    MessageDescriptions.PRODUCT_NOT_FOUND
                );

            await productRepositories.IncrementViewAsync(product.Id);

            var mapped = mapper.Map<ProductResponse>(product);

            return ApiResponse<ProductResponse>.Ok(
                mapped,
                MessageKeys.GET_PRODUCTS_SUCCESS,
                MessageDescriptions.GET_PRODUCTS_SUCCESS
            );
        }

        public async Task<ApiResponse<ProductResponse>> GetBySlugAsync(string slug)
        {
            var product = await productRepositories.GetBySlugAsync(slug)
                ?? throw new NotFoundException(
                    MessageKeys.PRODUCT_NOT_FOUND,
                    MessageDescriptions.PRODUCT_NOT_FOUND
                );

            var mapped = mapper.Map<ProductResponse>(product);

            return ApiResponse<ProductResponse>.Ok(
                mapped,
                MessageKeys.GET_SLUG_PRODUCT_SUCCESS,
                MessageDescriptions.GET_SLUG_PRODUCT_SUCCESS
            );
        }

        public async Task<ApiResponse<ProductResponse>> CreateAsync(ProductCreateRequest request)
        {
            // check category exists
            var category = await categoryRepositories.GetByIdAsync(request.CategoryId)
                ?? throw new NotFoundException(
                    MessageKeys.CATEGORY_NOT_FOUND, 
                    MessageDescriptions.CATEGORY_NOT_FOUND
                );

            // product name
            var name = request.Name.Trim();

            // normalize product name
            var normalizedName = name.ToLower();

            // check exists product name in same category
            var exists = await productRepositories.ExistsAsync(
                normalizedName,
                request.CategoryId
            );

            if (exists)
                throw new BadRequestException(
                    MessageKeys.PRODUCT_ALREADY_EXISTS,
                    MessageDescriptions.PRODUCT_ALREADY_EXISTS
                );

            var slug = SlugHelper.Generate(name);

            var slugExists = await productRepositories.SlugExistsAsync(slug);

            if(slugExists)
                slug = $"{slug}-{Guid.NewGuid().ToString()[..6]}";

            var product = mapper.Map<Product>(request);

            product.Name = name;
            product.NormalizedName = normalizedName;
            product.Slug = slug;

            // Seller tạo thì gán SellerId, Admin tạo thì để null
            if (currentUserService.Roles.Contains("Seller") && !currentUserService.Roles.Contains("Admin"))
                product.SellerId = currentUserService.UserId;

            var created = await productRepositories.CreateAsync(product);
            var response = mapper.Map<ProductResponse>(created);

            return ApiResponse<ProductResponse>.Ok(
                response, 
                MessageKeys.CREATE_PRODUCT_SUCCESS,
                MessageDescriptions.CREATE_PRODUCT_SUCCESS
            );
        }

        public async Task<ApiResponse<ProductResponse>> UpdateAsync(Guid id, ProductUpdateRequest request)
        {
            // check product exists
            var existing = await productRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(MessageKeys.PRODUCT_NOT_FOUND, MessageDescriptions.PRODUCT_NOT_FOUND);

            // Seller chỉ được sửa product của mình, Admin bypass
            if (!currentUserService.Roles.Contains("Admin"))
            {
                if (existing.SellerId != currentUserService.UserId)
                    throw new ForbiddenException(
                        MessageKeys.FORBIDDEN,
                        MessageDescriptions.FORBIDDEN);
            }

            // check category exists
            var category = await categoryRepositories.GetByIdAsync(request.CategoryId)
                ?? throw new NotFoundException(MessageKeys.CATEGORY_NOT_FOUND, MessageDescriptions.CATEGORY_NOT_FOUND);

            existing.Name = request.Name;
            existing.Description = request.Description;
            existing.Stock = request.Stock;
            existing.Price = request.Price;
            existing.CategoryId = request.CategoryId;

            await productRepositories.UpdateAsync(existing);

            var response = mapper.Map<ProductResponse>(existing);
            return ApiResponse<ProductResponse>.Ok(
                response,
                MessageKeys.UPDATE_PRODUCT_SUCCESS,
                MessageDescriptions.UPDATE_PRODUCT_SUCCESS
            );
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var product = await productRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(MessageKeys.PRODUCT_NOT_FOUND, MessageDescriptions.PRODUCT_NOT_FOUND);

            // Seller chỉ được xóa product của mình, Admin bypass
            if (!currentUserService.Roles.Contains("Admin"))
            {
                if (product.SellerId != currentUserService.UserId)
                    throw new ForbiddenException(
                        MessageKeys.FORBIDDEN,
                        MessageDescriptions.FORBIDDEN);
            }

            // Lấy kết quả từ repository
            var result = await productRepositories.DeleteAsync(product);

            if (!result)
            {
                return ApiResponse<bool>.Fail(
                    MessageKeys.DELETE_PRODUCT_FAILURE,
                    MessageDescriptions.DELETE_PRODUCT_FAILURE
                );
            }

            return ApiResponse<bool>.Ok(
                result,
                MessageKeys.DELETE_PRODUCT_SUCCESS,
                MessageDescriptions.DELETE_PRODUCT_SUCCESS
            );
        }

        public async Task<ApiResponse<int>> IncrementViewAsync(Guid id)
        {
            var product = await productRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(
                    MessageKeys.PRODUCT_NOT_FOUND,
                    MessageDescriptions.PRODUCT_NOT_FOUND
                );

            return ApiResponse<int>.Ok(
                product.ViewCount,
                MessageKeys.GET_PRODUCT_VIEW_SUCCESS,
                MessageDescriptions.GET_PRODUCT_VIEW_SUCCESS
            );
        }

    }
}