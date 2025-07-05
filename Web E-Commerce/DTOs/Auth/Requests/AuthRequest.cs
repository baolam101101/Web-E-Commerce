namespace Web_E_Commerce.DTOs.Auth.Requests
{
    public class AuthRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }
}
