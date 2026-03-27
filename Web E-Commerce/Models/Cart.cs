namespace Web_E_Commerce.Models
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = [];
    }
}