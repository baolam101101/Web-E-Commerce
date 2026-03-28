using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Cart.Requests;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Customer")]
    public class CartsController(ICartService cartService) : ControllerBase
    {
        [HttpGet("my-cart")]
        public async Task<IActionResult> GetMyCart()
        {
            var response = await cartService.GetMyCart();
            return Ok(response);
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var response = await cartService.AddToCart(request);
            return Ok(response);
        }

        [HttpPut("update-cart")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartRequest request)
        {
            var response = await cartService.UpdateCart(request);
            return Ok(response);
        }

        [HttpDelete("remove-item/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(Guid cartItemId)
        {
            var response = await cartService.RemoveItem(cartItemId);
            return Ok(response);
        }
    }
}
