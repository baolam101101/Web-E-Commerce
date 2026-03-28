using Microsoft.EntityFrameworkCore.Storage;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Repositories.Interfaces
{
    public interface IOrderRepositories
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<List<Order>> GetByUserIdAsync(Guid userId);
        Task<List<Order>> GetAllAsync(); // admin
        Task AddAsync(Order order);
        Task AddRangeItemsAsync(List<OrderItem> items);
        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
