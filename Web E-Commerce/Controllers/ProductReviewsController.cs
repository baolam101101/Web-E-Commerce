using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.ProductPreview.Requests;
using Web_E_Commerce.DTOs.ProductPreview.Responses;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/products/{productId:Guid}/reviews")]
    public class ProductReviewsController(IProductReviewService reviewService) : ControllerBase
    {
        // GET api/v1/products/{productId}/reviews — public
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResponse<ReviewSummaryResponse>> GetReviews(Guid productId)
        {
            return await reviewService.GetByProductIdAsync(productId);
        }

        // POST api/v1/products/{productId}/reviews — phải login và đã mua
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(
            Guid productId,
            [FromBody] CreateReviewRequest request)
        {
            var response = await reviewService.CreateAsync(productId, request);
            return StatusCode(201, response);
        }

        // PUT api/v1/products/{productId}/reviews/{reviewId} — owner only
        [Authorize]
        [HttpPut("{reviewId:Guid}")]
        public async Task<ApiResponse<ReviewResponse>> Update(
            Guid reviewId,
            [FromBody] UpdateReviewRequest request)
        {
            return await reviewService.UpdateAsync(reviewId, request);
        }

        // DELETE api/v1/products/{productId}/reviews/{reviewId} — owner only
        [Authorize]
        [HttpDelete("{reviewId:Guid}")]
        public async Task<IActionResult> Delete(Guid reviewId)
        {
            var response = await reviewService.DeleteAsync(reviewId);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}