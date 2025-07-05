namespace Web_E_Commerce.DTOs.Auth.Responses
{
    public class AuthResponse
    {
        public string Message { get; set; } = string.Empty;
        public AuthData Data { get; set; } = new AuthData();
    }

    public class AuthData
    {
        public string AccessToken { get; set; } = string.Empty;

    }
}
