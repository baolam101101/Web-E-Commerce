using Web_E_Commerce.Enums;

namespace Web_E_Commerce.DTOs.Admin.Orders
{
    public class OrderQueryRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public OrderStatus? OrderStatus { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }

        public Guid? UserId { get; set; }
    }
}
