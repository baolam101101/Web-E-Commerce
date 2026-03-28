using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface ICartItemRepositories
    {
        Task<List<CartItem>> GetByCartIdAsync(Guid cartId);
        Task<CartItem?> GetByIdAsync(Guid id);
        Task<CartItem?> GetByCartAndProductAsync(Guid cartId, Guid productId);

        Task AddAsync(CartItem item);
        Task UpdateAsync(CartItem item);
        Task RemoveAsync(CartItem item);
        Task RemoveRangeAsync(List<CartItem> items);
    }
}
