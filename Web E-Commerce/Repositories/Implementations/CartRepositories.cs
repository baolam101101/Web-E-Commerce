using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Repositories.Implementations
{
    public class CartRepositories(AppDbContext _context) : ICartRepositories
    {
        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart> AddItemToCartAsync(int userId, int product, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = []
                };
                _context.Carts.Add(cart);
            }

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == product);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = product,
                    Quantity = quantity
                });
            }

            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> UpdateCartItemQuantityAsync(int userId, int cartItemId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId) ?? throw new Exception("Cart not found");
            var cartItem = cart?.CartItems.FirstOrDefault(ci => ci.Id == cartItemId) ?? throw new Exception("Cart item not found");

            cartItem.Quantity = quantity;
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> RemoveItemFromCartAsync(int userId, int cartItemId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId) ?? throw new Exception("Cart not found");

            var cartItem = cart?.CartItems.FirstOrDefault(ci => ci.Id == cartItemId) ?? throw new Exception("Cart item not found");

            cart.CartItems.Remove(cartItem);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> ClearCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId) ?? throw new Exception("Cart not found");
            cart.CartItems.Clear();
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return cart;
        }
    }
}