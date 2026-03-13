using Web_E_Commerce.Enums;

namespace Web_E_Commerce.DTOs.Client.Product.Requests
{
    public class ProductFilterDto
    {
        public string? Keyword { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public ProductSortBy? SortBy { get; set; }

        // Pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
