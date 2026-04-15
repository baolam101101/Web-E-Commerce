namespace Web_E_Commerce.DTOs.Client.Profile.Requests
{
    public class UpdateProfileRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
