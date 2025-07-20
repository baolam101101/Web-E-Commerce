﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.SellerRequest.Responses;
using Web_E_Commerce.DTOs.Seller.Requests;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Models;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class SellerRequestService(
        ICurrentUserService currentUser,
        IMapper mapper,
        AppDbContext context) : ISellerRequestService
    {
        public async Task<ApiResponse<SellerRequestResponse>> RequestSellerAsync(SellerRequestDto dto)
        {
            var userId = currentUser.UserId;

            var exists = await context.SellerRequests
                .AnyAsync(r => r.UserId == userId && r.Status == "Pending");
            if (exists)
            {
                return new ApiResponse<SellerRequestResponse>(
                    "You already have a pending seller request",
                    null);
            }

            var request = mapper.Map<SellerRequest>(dto);
            request.UserId = userId;

            context.SellerRequests.Add(request);
            await context.SaveChangesAsync();

            var result = mapper.Map<SellerRequestResponse>(request);
            return new ApiResponse<SellerRequestResponse>("Request sent", result);
        }
    }
}