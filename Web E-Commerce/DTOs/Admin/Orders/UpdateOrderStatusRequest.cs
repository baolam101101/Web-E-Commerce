using Web_E_Commerce.Enums;

namespace Web_E_Commerce.DTOs.Admin.Orders
{
    public class UpdateOrderStatusRequest
    {
        public OrderStatus Status { get; set; }
    }
}
