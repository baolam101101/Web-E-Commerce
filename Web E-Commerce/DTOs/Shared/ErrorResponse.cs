namespace Web_E_Commerce.DTOs.Shared
{
    public class ErrorResponse(int statusCode, string code, string message, string? description = null)
    {
        public int StatusCode { get; set; } = statusCode;
        public string Code { get; set; } = code;
        public string Message { get; set; } = message;
        public string? Description { get; set; } = description;
    }
}
