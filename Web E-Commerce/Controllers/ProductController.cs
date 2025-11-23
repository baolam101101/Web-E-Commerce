using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var response = await productService.GetAllAsync(page, pageSize);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await productService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)
        {
            var response = await productService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateRequest request)
        {
            var response = await productService.UpdateAsync(id, request);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await productService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        //[AllowAnonymous]
        //[HttpGet("filter")]
        //public async Task<IActionResult> Filter([FromQuery] ProductFilterDto filterDto)
        //{
        //    var response = await productService.FilterAsync(filterDto);
        //    return Ok(response);
        //}
    }
}