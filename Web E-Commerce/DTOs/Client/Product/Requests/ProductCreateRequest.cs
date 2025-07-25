namespace Web_E_Commerce.DTOs.Client.Product.Requests
{
    public class ProductCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
