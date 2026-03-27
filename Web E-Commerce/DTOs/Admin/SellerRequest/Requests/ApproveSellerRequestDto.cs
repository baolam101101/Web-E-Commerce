namespace Web_E_Commerce.DTOs.Admin.SellerRequest.Requests
{
    public class ApproveSellerRequestDto
    {
        public Guid RequestId { get; set; }
        public bool Approve { get; set; }
    }
}
