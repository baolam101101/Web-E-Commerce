using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Models
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod {  get; set; }
        public PaymentStatus PaymentStatus { get; set; } 
        public string? TransactionId { get; set; } // Nếu tích hợp VNPay hoặc cổng thanh toán khác
        public string? GatewayResponse { get; set; }
    }
}
