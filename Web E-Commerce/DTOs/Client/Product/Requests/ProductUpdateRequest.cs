using System.ComponentModel.DataAnnotations;

namespace Web_E_Commerce.DTOs.Client.Product.Requests
{
    public class ProductUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
