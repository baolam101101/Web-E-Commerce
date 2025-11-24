using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Category.Requests;
using Web_E_Commerce.DTOs.Category.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var response = await categoryService.GetAllAsync(page, pageSize);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await categoryService.GetByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        //[Authorize(Policy = nameof(UserRole.Admin))]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateRequest dto)
        {
            var response = await categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateRequest dto)
        {
            var response = await categoryService.UpdateAsync(id, dto);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await categoryService.DeleteAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
