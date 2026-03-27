namespace Web_E_Commerce.DTOs.ProductImage.Responses
{
    public class ProductImageResponse
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}