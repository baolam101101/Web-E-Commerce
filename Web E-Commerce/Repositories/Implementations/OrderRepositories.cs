using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;
using Web_E_Commerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Web_E_Commerce.Repositories.Implementations
{
    public class OrderRepositories(AppDbContext _context) : IOrderRepositories
    {
        public async Task<IEnumerable<Order>> GetAllAsync() =>
            await _context.Orders.Include(o => o.User).ToListAsync();
        public async Task<Order?> GetByIdAsync(int id) =>
            await _context.Orders
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == id);
        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
