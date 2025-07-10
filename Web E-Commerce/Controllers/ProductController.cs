using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Product.Requests;
using Web_E_Commerce.DTOs.Product.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var products = await productService.GetAllAsync(page, pageSize);
            return Ok(new ApiResponse<IEnumerable<ProductCreateResponse>>("Fetched successfully", products));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await productService.GetByIdAsync(id);
            return product == null
                ? NotFound(new ErrorResponse("Product not found", 404, "PRODUCT_NOT_FOUND", $"No product with ID = {id}"))
                : Ok(new ApiResponse<ProductCreateResponse>("Success", product));
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            var created = await productService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new ApiResponse<ProductCreateResponse>("Created", created));
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateRequest request)
        {
            var updated = await productService.UpdateAsync(id, request);
            return updated == null
                ? NotFound(new ApiResponse<string>("Product not found", ""))
                : Ok(new ApiResponse<ProductUpdateResponse>("Updated", updated));
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await productService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
