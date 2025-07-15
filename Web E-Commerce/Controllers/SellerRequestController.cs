using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Seller.Requests;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/seller-requests")]
    [Authorize]
    public class SellerRequestController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RequestSeller([FromBody] SellerRequestDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var exists = await context.SellerRequests
                .AnyAsync(r => r.UserId == userId && r.Status == "Pending");

            if (exists)
            {
                return BadRequest("You already have a pending seller request");
            }

            var request = mapper.Map<SellerRequest>(dto);
            request.UserId = userId;

            context.SellerRequests.Add(request);
            await context.SaveChangesAsync();

            return Ok(new { message = "Seller request submitted successfully." });
        }
    }
}
