using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
