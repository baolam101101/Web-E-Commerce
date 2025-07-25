using System.ComponentModel.DataAnnotations;

namespace Web_E_Commerce.DTOs.Client.Product.Requests
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
