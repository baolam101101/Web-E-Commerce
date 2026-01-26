namespace Web_E_Commerce.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Audit fields
        public string CreatedBy { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; }
    }
}