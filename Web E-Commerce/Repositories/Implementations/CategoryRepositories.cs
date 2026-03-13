using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Repositories.Implementations
{
    public class CategoryRepositories(AppDbContext _context) : ICategoryRepositories
    {
        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

        public async Task<IEnumerable<Category>> GetAllActiveAsync() 
            => await _context.Categories
                .Where(c => c.IsActive) 
                .ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);

        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(string normalizedName)
        {
            return await _context.Categories
                .AnyAsync(c => c.NormalizedName == normalizedName);
        }
    }
}
