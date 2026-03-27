using Web_E_Commerce.Enums;

namespace Web_E_Commerce.DTOs.Order.Responses
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItemResponse> Items { get; set; } = [];
    }
}