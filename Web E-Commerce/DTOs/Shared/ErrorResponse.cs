namespace Web_E_Commerce.DTOs.Shared
{
    public class ErrorResponse(string message, int statusCode, string? code = null, string? description = null)
    {
        public string Message { get; set; } = message;
        public int StatusCode { get; set; } = statusCode;
        public string? Code { get; set; } = code;
        public string? Description { get; set; } = description;
    }
}
