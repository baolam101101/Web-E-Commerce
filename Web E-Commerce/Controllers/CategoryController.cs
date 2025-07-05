using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Category.Requests;
using Web_E_Commerce.DTOs.Category.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController(ICategoryRepositories categoryRepositories) : ControllerBase
    {
        private readonly ICategoryRepositories _categoryRepositories = categoryRepositories;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var allCategory = await _categoryRepositories.GetAllAsync();

            var pageCategories = allCategory
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var pagination = new PaginationWrapper<Category>(
                items: pageCategories,
                page: page,
                pageSize: pageSize,
                totalItems: allCategory.Count());

            var response = new ApiResponse<PaginationWrapper<Category>>(
                message: "Get all categories successfully!",
                data: pagination);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepositories.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var response = new CategoryResponse()
            {
                Id = category.Id,
                Name = category.Name,
            };
            return Ok(new ApiResponse<CategoryResponse>("Get category detail successfully!", response));
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateRequest categoryCreateRequest)
        {
            var category = new Category
            {
                Name = categoryCreateRequest.Name
            };
            var created = await _categoryRepositories.CreateAsync(category);

            var response = new CategoryCreateResponse()
            {
                Id = category.Id,
                Name = category.Name,
            };
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new ApiResponse<CategoryCreateResponse>("Category created successfully!", response));
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpPut("id")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            category.Id = id;

            await _categoryRepositories.UpdateAsync(category);
            return Ok(new { message = "You have updated successfully!", category});
        }

        [Authorize(Policy = nameof(UserRole.Admin))]
        [HttpDelete("id")]
        public async Task<IActionResult> Delete (int id)
        {
            var category = await _categoryRepositories.GetByIdAsync(id);
            if (category == null) return NotFound();
            await _categoryRepositories.DeleteAsync(category);
            return NoContent();
        }
    }
}
