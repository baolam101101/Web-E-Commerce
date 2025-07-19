namespace Web_E_Commerce.DTOs.Shared
{
    public class ApiResponse<T>(string message, T? data)
    {
        public string Message { get; set; } = message;
        public T? Data { get; set; } = data;
    }
}