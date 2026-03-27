using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Models
{
    public class SellerRequest : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public string ShopName { get; set; } = string.Empty;
        public SellerRequestStatus Status { get; set; } = SellerRequestStatus.Pending;
        public DateTime RequestAt { get; set; } = DateTime.UtcNow;
        public string? Note { get; set; }
    }
}