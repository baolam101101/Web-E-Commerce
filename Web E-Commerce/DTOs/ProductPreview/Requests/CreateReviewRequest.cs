namespace Web_E_Commerce.DTOs.ProductPreview.Requests
{
    public class CreateReviewRequest
    {
        public int Rating { get; set; }       // 1 - 5
        public string Comment { get; set; } = string.Empty;
    }
}
