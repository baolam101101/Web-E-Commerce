namespace Web_E_Commerce.DTOs.Shared
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? Description { get; set; }

        public ErrorResponse() { }

        public ErrorResponse(int statusCode, string? code, string? message, string? description)
        {
            StatusCode = statusCode;
            Code = code;
            Message = message;
            Description = description;
        }
    }

}
