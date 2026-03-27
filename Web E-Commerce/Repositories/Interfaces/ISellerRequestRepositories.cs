using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface ISellerRequestRepositories
    {
        public Task<IQueryable<SellerRequest>> GetQueryableAsync();

        Task<bool> ExistsPendingByUserAsync(Guid userId);

        Task AddAsync(SellerRequest request);

        Task<SellerRequest?> GetByIdWithUserAsync(Guid requestId);

        Task<SellerRequest?> GetByIdAsync(Guid requestId);

        Task SaveChangesAsync();
    }
}
