namespace Web_E_Commerce.DTOs.ProductPreview.Requests
{
    public class UpdateReviewRequest
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
