using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface ISellerRequestRepositories
    {
        public Task<IQueryable<SellerRequest>> GetQueryableAsync();

        Task<bool> ExistsPendingByUserAsync(int userId);

        Task AddAsync(SellerRequest request);

        Task<SellerRequest?> GetByIdWithUserAsync(int requestId);

        Task<SellerRequest?> GetByIdAsync(int requestId);

        Task SaveChangesAsync();
    }
}
