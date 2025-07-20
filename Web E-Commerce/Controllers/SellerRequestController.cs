using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Seller.Requests;
using Web_E_Commerce.Models;
using Web_E_Commerce.Services.Implementations;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/v1/seller-requests")]
    [Authorize]
    public class SellerRequestController(ISellerRequestService sellerRequestService ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RequestSeller([FromBody] SellerRequestDto dto)
        {
            var result = await sellerRequestService.RequestSellerAsync(dto);
            return Ok(result);
        }
    }
}