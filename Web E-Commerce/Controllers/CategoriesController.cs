using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Category.Requests;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var response = await categoryService.GetAllAsync(
                page,
                pageSize,
                includeInactive: false
            );

            return response.Success ? Ok(response) : NotFound(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<IActionResult> GetAllForAdmin(int page = 1, int pageSize = 10)
        {
            var response = await categoryService.GetAllAsync(
                page,
                pageSize,
                includeInactive: true
            );

            return response.Success ? Ok(response) : NotFound(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
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
        public async Task<IActionResult> Update(Guid id, CategoryUpdateRequest dto)
        {
            var response = await categoryService.UpdateAsync(id, dto);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var response = await categoryService.DeactivateAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var response = await categoryService.ActivateAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}