namespace Web_E_Commerce.DTOs.Shared
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Description { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message, string description)
            => new ApiResponse<T> { Success = true, Message = message, Description = description, Data = data };
        public static ApiResponse<T> Fail(string message, string description)
            => new ApiResponse<T> { Success = false, Message = message, Description = description };
    }
}