using AutoMapper;
using Web_E_Commerce.DTOs.Product.Requests;
using Web_E_Commerce.DTOs.Product.Responses;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class ProductService(
        IProductRepository productRepository,
        IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductCreateResponse>> GetAllAsync(int page, int pageSize)
        {
            var all = await productRepository.GetAllAsync();
            var paged = all.Skip((page - 1) * pageSize).Take(pageSize);
            return mapper.Map<IEnumerable<ProductCreateResponse>>(paged);
        }

        public async Task<ProductCreateResponse?> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            return product is null ? null : mapper.Map<ProductCreateResponse>(product);
        }

        public async Task<ProductCreateResponse> CreateAsync(ProductCreateRequest request)
        {
            var product = mapper.Map<Product>(request);
            var created = await productRepository.CreateAsync(product);
            return mapper.Map<ProductCreateResponse>(created);
        }

        public async Task<ProductUpdateResponse?> UpdateAsync(int id, ProductUpdateRequest request)
        {
            var existing = await productRepository.GetByIdAsync(id);
            if (existing is null) return null;

            existing.Name = request.Name;
            existing.Description = request.Description;
            existing.ImageUrl = request.ImageUrl;
            existing.Price = request.Price;
            existing.CategoryId = request.CategoryId;

            await productRepository.UpdateAsync(existing);
            return mapper.Map<ProductUpdateResponse>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null) return false;

            // Lấy kết quả từ repository
            var result = await productRepository.DeleteAsync(product);
            return result;
        }

    }
}