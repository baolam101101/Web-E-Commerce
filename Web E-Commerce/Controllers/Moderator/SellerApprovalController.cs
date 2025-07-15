using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.SellerRequest.Requests;
using Web_E_Commerce.DTOs.Admin.SellerRequest.Responses;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Controllers.Moderator
{
    [ApiController]
    [Route("api/v1/moderator/seller-requests")]
    [Authorize(Roles = "Admin, Moderator")]
    public class SellerApprovalController(AppDbContext context, IMapper mapper) : ControllerBase
    {
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRequests()
        {
            var pending = await context.SellerRequests
                .Where(r => r.Status == "Pending")
                .Include(r => r.User)
                .ToArrayAsync();

            var mapped = mapper.Map<List<SellerRequestResponse>>(pending);

            return Ok(new { message = "Pending seller requests", data = mapped });
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveRequest([FromBody] ApproveSellerRequestDto dto)
        {
            var request = await context.SellerRequests
                .Include(r => r.User)
                .ThenInclude(u => u.UserRoles)
                .FirstOrDefaultAsync(r => r.Id == dto.RequestId);

            if (request == null || request.Status != "Pending")
            {
                return BadRequest("Invalid or already processed request");
            }

            if (dto.Approve)
            {
                var sellerRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Seller");
                if (sellerRole == null)
                    return BadRequest("Seller role not found");

                if (!request.User!.UserRoles.Any(ur => ur.RoleId == sellerRole.Id))
                {
                    request.User.UserRoles.Add(new UserRole
                    {
                        UserId = request.User.Id,
                        RoleId = sellerRole.Id
                    });
                }
            }

            await context.SaveChangesAsync();

            return Ok(new { message = dto.Approve ? "Seller request approved" : "Seller request rejected" });
        }
    }
}
