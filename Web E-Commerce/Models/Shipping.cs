using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Models
{
    public class Shipping : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public string ReceiverName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string? TrackingCode { get; set; }
        public string? ShippingProvider { get; set; }

        public ShippingStatus Status { get; set; }

        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }
}
