using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface ICartRepositories
    {
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task SaveChangesAsync();
    }
}