namespace Web_E_Commerce.DTOs.Auth.Responses
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}