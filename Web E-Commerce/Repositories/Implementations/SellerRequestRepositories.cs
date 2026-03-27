using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Repositories.Implementations
{
    public class SellerRequestRepositories(AppDbContext context)
       : ISellerRequestRepositories
    {
        public Task<IQueryable<SellerRequest>> GetQueryableAsync()
        {
            var query = context.SellerRequests
                .Include(r => r.User)
                .AsQueryable();

            return Task.FromResult(query);
        }

        public async Task<bool> ExistsPendingByUserAsync(Guid userId)
        {
            return await context.SellerRequests
                .AnyAsync(r =>
                    r.UserId == userId &&
                    r.Status == SellerRequestStatus.Pending);
        }

        public async Task AddAsync(SellerRequest request)
        {
            await context.SellerRequests.AddAsync(request);
        }

        public async Task<SellerRequest?> GetByIdWithUserAsync(Guid requestId)
        {
            return await context.SellerRequests
                .Include(r => r.User)
                .ThenInclude(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }

        public async Task<SellerRequest?> GetByIdAsync(Guid requestId)
        {
            return await context.SellerRequests
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}