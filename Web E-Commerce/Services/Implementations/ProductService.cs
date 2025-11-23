using AutoMapper;
using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Client.Product.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class ProductService(
        IProductRepositories productRepositories,
        ICategoryRepositories categoryRepositories,
        IMapper mapper) : IProductService
    {
        public async Task<ApiResponse<PaginationWrapper<ProductResponse>>> GetAllAsync(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                throw new BadRequestException(MessageKeys.INVALID_PAGINATION_PARAMETERS, MessageDescriptions.INVALID_PAGINATION_PARAMETERS);

            var query = productRepositories.GetAllAsync();

            var queryable = await query;

            var totalItems = queryable.Count();

            var items = queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mapped = mapper.Map<IEnumerable<ProductResponse>>(items);

            var pagination = new PaginationWrapper<ProductResponse>(
                page,
                pageSize,
                totalItems,
                mapped
            );

            return ApiResponse<PaginationWrapper<ProductResponse>>.Ok(
                pagination,
                MessageKeys.GET_ALL_PRODUCTS_SUCCESS,
                MessageDescriptions.GET_ALL_PRODUCTS_SUCCESS
            );
        }

        public async Task<ApiResponse<ProductResponse>> GetByIdAsync(int id)
        {
            var product = await productRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(MessageKeys.PRODUCT_NOT_FOUND, MessageDescriptions.PRODUCT_NOT_FOUND);

            var mapped = mapper.Map<ProductResponse>(product);

            return ApiResponse<ProductResponse>.Ok(
                mapped,
                MessageKeys.GET_PRODUCTS_SUCCESS,
                MessageDescriptions.GET_PRODUCTS_SUCCESS
            );
        }

        public async Task<ApiResponse<ProductResponse>> CreateAsync(ProductCreateRequest request)
        {
            // check category exists
            var category = await categoryRepositories.GetByIdAsync(request.CategoryId)
                ?? throw new NotFoundException(MessageKeys.CATEGORY_NOT_FOUND, MessageDescriptions.CATEGORY_NOT_FOUND);

            var product = mapper.Map<Product>(request);
            var created = await productRepositories.CreateAsync(product);
            var response = mapper.Map<ProductResponse>(created);

            return ApiResponse<ProductResponse>.Ok(
                response, 
                MessageKeys.CREATE_PRODUCT_SUCCESS,
                MessageDescriptions.CREATE_PRODUCT_SUCCESS
            );
        }

        public async Task<ApiResponse<ProductResponse>> UpdateAsync(int id, ProductUpdateRequest request)
        {
            // check product exists
            var existing = await productRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(MessageKeys.PRODUCT_NOT_FOUND, MessageDescriptions.PRODUCT_NOT_FOUND);

            // check category exists
            var category = await categoryRepositories.GetByIdAsync(request.CategoryId)
                ?? throw new NotFoundException(MessageKeys.CATEGORY_NOT_FOUND, MessageDescriptions.CATEGORY_NOT_FOUND);

            existing.Name = request.Name;
            existing.Description = request.Description;
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

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var product = await productRepositories.GetByIdAsync(id)
                ?? throw new NotFoundException(MessageKeys.PRODUCT_NOT_FOUND, MessageDescriptions.PRODUCT_NOT_FOUND);

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

        //public async Task<ApiResponse<PaginationWrapper<ProductResponse>>> FilterAsync(ProductFilterDto filterDto)
        //{
        //    var (items, totalCount) = await productRepositories.FilterAsync(
        //        filterDto.CategoryId,
        //        filterDto.Keyword,
        //        filterDto.MinPrice,
        //        filterDto.MaxPrice,
        //        filterDto.SortBy,
        //        filterDto.Page,
        //        filterDto.PageSize
        //    );

        //    // Map từ Product -> ProductResponse
        //    var mappedItems = mapper.Map<IEnumerable<ProductResponse>>(items);

        //    var response = new PaginationWrapper<ProductResponse>(
        //        filterDto.Page,
        //        filterDto.PageSize,
        //        totalCount,
        //        mappedItems
        //    );

        //    return new ApiResponse<PaginationWrapper<ProductResponse>>(
        //        "Filtered products successfully",
        //        response
        //    );
        //}

    }
}