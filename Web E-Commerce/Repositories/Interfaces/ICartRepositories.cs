using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface ICartRepositories
    {
        public Task<Cart?> GetCartByUserIdAsync(int userId);
        public Task<Cart> AddItemToCartAsync(int userId, int productId, int quantity);
        public Task<Cart> UpdateCartItemQuantityAsync(int userId, int cartItemId, int quantity);
        public Task<Cart> RemoveItemFromCartAsync(int userId, int cartItemId);
        public Task<Cart> ClearCartAsync(int userId);
    }
}
