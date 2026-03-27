using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Admin.SellerRequest.Responses;
using Web_E_Commerce.DTOs.Seller.Requests;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.DTOs.Shared.Constants;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Implementations;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Services.Interfaces;

namespace Web_E_Commerce.Services.Implementations
{
    public class SellerRequestService(
    ICurrentUserService currentUser,
    IMapper mapper,
    ISellerRequestRepositories sellerRequestRepositories,
    IRoleRepositories roleRepositories
) : ISellerRequestService
    {
        public async Task<ApiResponse<PaginationWrapper<SellerRequestResponse>>> GetAllAsync(
            SellerRequestStatus? status,
            int page,
            int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = await sellerRequestRepositories.GetQueryableAsync();

            if (status.HasValue)
                query = query.Where(r => r.Status == status.Value);

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.RequestAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mapped = mapper.Map<IEnumerable<SellerRequestResponse>>(items);

            var pagination = new PaginationWrapper<SellerRequestResponse>(
                page,
                pageSize,
                totalItems,
                mapped
            );

            return ApiResponse<PaginationWrapper<SellerRequestResponse>>.Ok(
                pagination,
                MessageKeys.GET_ALL_SELLER_REQUESTS_SUCCESS,
                MessageDescriptions.GET_ALL_SELLER_REQUESTS_SUCCESS
            );
        }

        public async Task<ApiResponse<SellerRequestResponse>> RequestSellerAsync(SellerRequestDto dto)
        {
            var userId = currentUser.UserId;

            if (await sellerRequestRepositories.ExistsPendingByUserAsync(userId))
            {
                return ApiResponse<SellerRequestResponse>.Fail(
                    MessageKeys.SELLER_REQUEST_PENDING,
                    MessageDescriptions.SELLER_REQUEST_PENDING
                );
            }

            var request = mapper.Map<SellerRequest>(dto);
            request.UserId = userId;

            await sellerRequestRepositories.AddAsync(request);
            await sellerRequestRepositories.SaveChangesAsync();

            var requestWithUser =
                await sellerRequestRepositories.GetByIdWithUserAsync(request.Id);

            var result = mapper.Map<SellerRequestResponse>(requestWithUser);

            return ApiResponse<SellerRequestResponse>.Ok(
                result,
                MessageKeys.SELLER_REQUEST_SENT,
                MessageDescriptions.SELLER_REQUEST_SENT
            );
        }

        public async Task<ApiResponse<bool>> ApproveAsync(Guid requestId)
        {
            var request = await sellerRequestRepositories.GetByIdWithUserAsync(requestId);

            if (request == null || request.Status != SellerRequestStatus.Pending)
            {
                return ApiResponse<bool>.Fail(
                    MessageKeys.INVALID_SELLER_REQUEST,
                    MessageDescriptions.INVALID_SELLER_REQUEST
                );
            }

            var sellerRole = await roleRepositories.GetByNameAsync("Seller");
            if (sellerRole == null)
                throw new Exception("Seller role not configured");

            var hasSellerRole = request.User!.UserRoles
                .Any(ur => ur.Role.Name == "Seller");

            if (!hasSellerRole)
            {
                request.User.UserRoles.Add(new UserRole
                {
                    UserId = request.User.Id,
                    RoleId = sellerRole.Id
                });
            }

            request.Status = SellerRequestStatus.Approved;
            request.RequestAt = DateTime.UtcNow;

            await sellerRequestRepositories.SaveChangesAsync();

            return ApiResponse<bool>.Ok(
                true,
                MessageKeys.SELLER_REQUEST_APPROVED,
                MessageDescriptions.SELLER_REQUEST_APPROVED
            );
        }

        public async Task<ApiResponse<bool>> RejectAsync(Guid requestId)
        {
            var request = await sellerRequestRepositories.GetByIdAsync(requestId);

            if (request == null || request.Status != SellerRequestStatus.Pending)
            {
                return ApiResponse<bool>.Fail(
                    MessageKeys.INVALID_SELLER_REQUEST,
                    MessageDescriptions.INVALID_SELLER_REQUEST
                );
            }

            request.Status = SellerRequestStatus.Rejected;
            request.RequestAt = DateTime.UtcNow;

            await sellerRequestRepositories.SaveChangesAsync();

            return ApiResponse<bool>.Ok(
                true,
                MessageKeys.SELLER_REQUEST_REJECTED,
                MessageDescriptions.SELLER_REQUEST_REJECTED
            );
        }
    }
}