namespace Web_E_Commerce.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int Rating { get; set; } // 1 - 5 stars
        public string Comment { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }
}