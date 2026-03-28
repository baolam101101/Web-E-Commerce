using Web_E_Commerce.Enums;

namespace Web_E_Commerce.DTOs.Order.Responses
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public List<OrderItemResponse> Items { get; set; } = [];
    }
}