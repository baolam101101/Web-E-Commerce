namespace Web_E_Commerce.DTOs.Client.Profile.Responses
{
    public class UserProfileResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public List<string> Roles { get; set; } = [];
        public DateTime CreatedAt { get; set; }
    }
}
