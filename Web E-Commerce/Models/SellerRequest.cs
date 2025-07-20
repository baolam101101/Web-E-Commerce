namespace Web_E_Commerce.Models
{
    public class SellerRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public string ShopName { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; // Pending | Approved | Rejected
        public DateTime RequestAt { get; set; } = DateTime.UtcNow;
        public string? Note { get; set; }
    }
}