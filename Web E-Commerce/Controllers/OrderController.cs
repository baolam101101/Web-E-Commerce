using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Order.Requests;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController(IOrderRepositories orderRepositories) : ControllerBase
    {
        private readonly IOrderRepositories _orderRepositories = orderRepositories;

        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IActionResult> Checkout(int userId, [FromBody] OrderCheckoutRequest orderCheckoutRequest)
        //{
        //    // Load the cart items for the user
        //    var cartItems = await _orderRepositories.Carts.Include(userId);
        //}
    }
}
