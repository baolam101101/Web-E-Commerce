namespace Web_E_Commerce.DTOs.Cart.Responses
{
    public class CartResponse
    {
        public List<CartItemResponse> Items { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}