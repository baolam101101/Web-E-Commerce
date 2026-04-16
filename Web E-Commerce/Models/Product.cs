using Microsoft.EntityFrameworkCore;

namespace Web_E_Commerce.Models
{
    [Index(nameof(NormalizedName), nameof(CategoryId), IsUnique = true)]
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public decimal Price { get; set; }
        public int ViewCount { get; set; }
        public int Stock { get; set; }
        public int Sold { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        // Null = tạo bởi Admin, có giá trị = tạo bởi Seller
        public Guid? SellerId { get; set; }
        public User? Seller { get; set; }
    }
}