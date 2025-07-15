namespace Web_E_Commerce.DTOs.ProductImage.Requests
{
    public class ProductImageUploadRequest
    {
        public IFormFile Image { get; set; } = null!; // Using null-forgiving operator since this will be validated
        public bool IsMainImage { get; set; } = false; // Indicates if this image is the main image for the product
        public string CreatedBy { get; set; } = string.Empty; // User who is uploading the image
    }
}
