namespace Web_E_Commerce.DTOs.Auth.Requests
{
    public class LogoutRequest
    {
        public string RefreshToken { get; set; } = default!;
    }
}
