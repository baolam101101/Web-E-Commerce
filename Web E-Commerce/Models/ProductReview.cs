namespace Web_E_Commerce.Models
{
    public class ProductReview : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public int Rating { get; set; } // 1 - 5 stars
        public string Comment { get; set; } = string.Empty;
    }
}