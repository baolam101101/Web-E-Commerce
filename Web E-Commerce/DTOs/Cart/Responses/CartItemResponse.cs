namespace Web_E_Commerce.DTOs.Cart.Responses
{
    public class CartItemResponse
    {
        public Guid CartItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal PriceAtTime { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => PriceAtTime * Quantity;
    }
}