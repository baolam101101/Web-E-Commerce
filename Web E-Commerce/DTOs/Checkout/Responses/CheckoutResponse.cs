using Web_E_Commerce.DTOs.Order.Responses;

namespace Web_E_Commerce.DTOs.Checkout.Responses
{
    public class CheckoutResponse
    {
        public OrderResponse Order { get; set; } = default!;
        public string? PaymentUrl { get; set; }

    }
}