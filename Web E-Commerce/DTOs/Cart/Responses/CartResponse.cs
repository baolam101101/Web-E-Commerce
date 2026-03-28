namespace Web_E_Commerce.DTOs.Cart.Responses
{
    public class CartResponse
    {
        public Guid CartId { get; set; }
        public List<CartItemResponse> Items { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}