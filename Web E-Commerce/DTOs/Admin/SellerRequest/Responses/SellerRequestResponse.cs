namespace Web_E_Commerce.DTOs.Admin.SellerRequest.Responses
{
    public class SellerRequestResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ShopName { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; }
    }
}
