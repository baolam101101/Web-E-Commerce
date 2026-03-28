using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.DTOs.Order.Requests;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        // CUSTOMER
        [Authorize(Roles = "Customer")]
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(OrderCheckoutRequest request)
        {
            var response = await orderService.Checkout(request);
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var response = await orderService.GetMyOrders();
            return Ok(response);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await orderService.GetByIdAsync(id);
            return Ok(response);
        }

        // ADMIN
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // optional nếu bạn làm admin dashboard
            return Ok();
        }
    }
}