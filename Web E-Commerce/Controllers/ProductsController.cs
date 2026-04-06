using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Client.Product.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Services.Implementations;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResponse<PagedResult<ProductResponse>>> GetProducts(
            [FromQuery] ProductFilterDto filter)
        {
            return await productService.GetProductsAsync(filter);
        }

        [AllowAnonymous]
        [HttpGet("{slug}/related")]
        public async Task<ApiResponse<List<ProductResponse>>> GetRelatedProducts(string slug)
        {
            return await productService.GetRelatedProductsAsync(slug);
        }

        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await productService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<ApiResponse<ProductResponse>> GetBySlug(string slug)
        {
            return await productService.GetBySlugAsync(slug);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/view")]
        public async Task<ApiResponse<int>> IncreaseView(Guid id)
        {
            return await productService.IncrementViewAsync(id);
        }

        [Authorize(Roles = "Admin, Seller")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            var response = await productService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }

        [Authorize(Roles = "Admin, Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateRequest request)
        {
            var response = await productService.UpdateAsync(id, request);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [Authorize(Roles = "Admin, Seller")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await productService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}