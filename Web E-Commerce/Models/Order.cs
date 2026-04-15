using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Models
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? PaidAt { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Shipping Shipping { get; set; } = default!;
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}