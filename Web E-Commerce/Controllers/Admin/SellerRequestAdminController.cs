using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Services.Implementations;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers.Admin
{
    [ApiController]
    [Route("api/v1/admin/seller-requests")]
    [Authorize(Roles = "Admin")]
    public class SellerRequestAdminController(ISellerRequestService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] SellerRequestStatus? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await service.GetAllAsync(status, page, pageSize);
            return Ok(response);
        }


        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(Guid id)
        => Ok(await service.ApproveAsync(id));

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(Guid id)
            => Ok(await service.RejectAsync(id));
    }
}
