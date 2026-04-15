using Web_E_Commerce.Enums;

namespace Web_E_Commerce.DTOs.Order.Requests
{
    public class OrderCheckoutRequest
    {
        public string ShippingAddress { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string ReceiverName { get; set; } = default!;
        public PaymentMethod PaymentMethod { get; set; }
    }
}