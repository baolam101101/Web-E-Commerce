using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Client.Product.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Exceptions;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper) : IProductService
    {
        public async Task<ApiResponse<PaginationWrapper<ProductResponse>>> GetAllAsync(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                throw new BadRequestException("Invalid pagination parameters", "Page and PageSize must be greater than 0.");

            var query = productRepository.GetAllQueryable(); // Trả về IQueryable<Product>

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mapped = mapper.Map<IEnumerable<ProductResponse>>(items);

            var pagination = new PaginationWrapper<ProductResponse>(
                page,
                pageSize,
                totalItems,
                mapped
            );

            return new ApiResponse<PaginationWrapper<ProductResponse>>(
                "Get all products successfully",
                pagination
            );
        }

        public async Task<ApiResponse<ProductResponse?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Product not found", $"No product found with ID {id}.");

            var mapped = mapper.Map<ProductResponse>(product);

            return new ApiResponse<ProductResponse?>(
                "Get product successfully",
                mapped
            );
        }

        public async Task<ApiResponse<ProductResponse>> CreateAsync(ProductCreateRequest request)
        {
            // check category exists
            var category = await categoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new NotFoundException("Category not found", $"Category with ID {request.CategoryId} does not exist.");

            var product = mapper.Map<Product>(request);
            var created = await productRepository.CreateAsync(product);
            var response = mapper.Map<ProductResponse>(created);

            return new ApiResponse<ProductResponse>(
                "Product created successfully",
                response
            );
        }

        public async Task<ApiResponse<ProductResponse?>> UpdateAsync(int id, ProductUpdateRequest request)
        {
            // check product exists
            var existing = await productRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Product not found", $"No product found with ID {id}.");

            // check category exists
            var category = await categoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new NotFoundException("Category not found", $"Category with ID {request.CategoryId} does not exist.");

            existing.Name = request.Name;
            existing.Description = request.Description;
            existing.Price = request.Price;
            existing.CategoryId = request.CategoryId;

            await productRepository.UpdateAsync(existing);

            var response = mapper.Map<ProductResponse>(existing);
            return new ApiResponse<ProductResponse?>(
                "Product updated successfully",
                response
            );
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Product not found", $"Cannot delete. No product with ID {id}.");

            // Lấy kết quả từ repository
            var result = await productRepository.DeleteAsync(product);

            return new ApiResponse<bool>(
                result ? "Product deleted successfully" : "Failed to delete product",
                result
            );
        }
    }
}