using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Category.Responses;
using Web_E_Commerce.DTOs.Product.Requests;
using Web_E_Commerce.DTOs.Product.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var allProducts = await _productRepository.GetAllAsync();

            var pagedProducts = allProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var pagination = new PaginationWrapper<Product>(
                items: pagedProducts,
                page: page,
                pageSize: pageSize,
                totalItems: allProducts.Count());

            var response = new ApiResponse<PaginationWrapper<Product>>(
                message: "Get all products successfully!",
                data: pagination);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound( new ErrorResponse(
                    message: "Product not found",
                    statusCode: Status404NotFound,
                    code: "PRODUCT_NOT_FOUND",
                    description: $"No product with Id = {id} found in the system."
                    ));
            }
            var response = new ProductCreateResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category == null ? null : new CategoryResponse()
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                },
            };

            return Ok(new ApiResponse<ProductCreateResponse>("Get product detail successfully!", response));
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest productCreateRequest)
        {
            var product = new Product
            {
                Name = productCreateRequest.Name,
                Description = productCreateRequest.Description,
                Price = productCreateRequest.Price,
                ImageUrl = productCreateRequest.ImageUrl,
                CategoryId = productCreateRequest.CategoryId,
            };

            var created = await _productRepository.CreateAsync(product);

            var response = new ProductCreateResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category == null ? null: new CategoryResponse()
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                }
            };
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new ApiResponse<ProductCreateResponse>("Product created successfully!", response));
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateRequest productUpdateRequest)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);

            if (existingProduct == null)
            {
                return NotFound(new ApiResponse<string>("Product not found", ""));
            }

            existingProduct.Name = productUpdateRequest.Name;
            existingProduct.Description = productUpdateRequest.Description;
            existingProduct.Price = productUpdateRequest.Price;
            existingProduct.ImageUrl = productUpdateRequest.ImageUrl;
            existingProduct.CategoryId = productUpdateRequest.CategoryId;

            await _productRepository.UpdateAsync(existingProduct);

            // Chuẩn bị dữ liệu trả về (Category có thể null)
            var response = new ProductUpdateResponse
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
                ImageUrl = existingProduct.ImageUrl,
                Category = existingProduct.Category == null
                    ? null
                    : new CategoryResponse
                    {
                        Id = existingProduct.Category.Id,
                        Name = existingProduct.Category.Name
                    }
            };

            return Ok(new ApiResponse<ProductUpdateResponse>("Updated successfully!", response));
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();
            await _productRepository.DeleteAsync(product);
            return NoContent();
        }
    }
}