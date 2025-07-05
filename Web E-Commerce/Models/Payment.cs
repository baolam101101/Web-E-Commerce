using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public PaymentMethod PaymentMethod {  get; set; }
        public DateTime PaidAt { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; } 
    }
}
