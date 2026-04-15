namespace Web_E_Commerce.DTOs.ProductPreview.Responses
{
    public class ReviewSummaryResponse
    {
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public List<ReviewResponse> Reviews { get; set; } = [];
    }
}
