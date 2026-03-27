using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Models
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? PaidAt { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}