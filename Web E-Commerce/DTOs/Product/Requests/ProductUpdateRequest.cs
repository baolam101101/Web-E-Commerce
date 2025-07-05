using System.ComponentModel.DataAnnotations;

namespace Web_E_Commerce.DTOs.Product.Requests
{
    public class ProductUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
        [Url]
        public string ImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
