namespace Web_E_Commerce.DTOs.Client.Product.Requests
{
    public class ProductFilterDto
    {
        public int? CategoryId { get; set; }
        public string? Keyword { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; }

        // Pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
