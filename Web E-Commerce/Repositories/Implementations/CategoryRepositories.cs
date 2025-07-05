using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Repositories.Implementations
{
    public class CategoryRepositories(AppDbContext context) : ICategoryRepositories
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

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

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
