using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Data;
using Web_E_Commerce.DTOs.Client.Product.Requests;
using Web_E_Commerce.DTOs.Shared;
using Web_E_Commerce.Enums;
using Web_E_Commerce.Models;
using Web_E_Commerce.Repositories.Interfaces;

namespace Web_E_Commerce.Repositories.Implementations
{
    public class ProductRepositories(AppDbContext _context) : IProductRepositories
    {
        public IQueryable<Product> GetQueryable()
        {
            return _context.Products
                .AsNoTracking()
                .Include(p => p.Category);
        }

        public async Task<List<Product>> GetRelatedProductsAsync(
            Guid categoryId,
            Guid excludeProductId)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p =>
                    p.CategoryId == categoryId &&
                    p.Id != excludeProductId)
                .OrderByDescending(p => p.CreatedAt)
                .Take(6)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task IncrementViewAsync(string slug)
        {
            await _context.Products
                .Where(p => p.Slug == slug)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(p => p.ViewCount, p => p.ViewCount + 1));
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string normalizedName, Guid categoryId)
        {
            return await _context.Products
                .AnyAsync(p =>
                    p.NormalizedName == normalizedName &&
                    p.CategoryId == categoryId
                );
        }
        public async Task<bool> SlugExistsAsync(string slug)
        {
            return await _context.Products
                .AnyAsync(p => p.Slug == slug);
        }

        public async Task<Product?> GetBySlugAsync(string slug)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<PagedResult<Product>> GetProductsAsync(ProductFilterDto filter)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // Keyword
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                var keyword = filter.Keyword.Trim().ToLower();

                query = query.Where(p => p.NormalizedName.Contains(keyword));
            }

            // Category
            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId);
            }

            // Min price
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice);
            }

            // Max price
            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice);
            }

            // Sorting
            query = filter.SortBy switch
            {
                ProductSortBy.PriceAsc => query.OrderBy(p => p.Price),
                ProductSortBy.PriceDesc => query.OrderByDescending(p => p.Price),
                ProductSortBy.NameAsc => query.OrderBy(p => p.Name),
                ProductSortBy.NameDesc => query.OrderByDescending(p => p.Name),
                ProductSortBy.Newest => query.OrderByDescending(p => p.CreatedAt),
                _ => query.OrderByDescending(p => p.CreatedAt)
            };

            // Total count
            var totalCount = await query.CountAsync();

            // Pagination
            var items = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<Product>(items, totalCount);
        }

        public async Task<List<Product>> GetByIdsAsync(List<Guid> ids)
        {
            return await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }
    }
}
