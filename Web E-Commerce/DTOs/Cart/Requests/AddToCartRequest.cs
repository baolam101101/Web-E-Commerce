namespace Web_E_Commerce.DTOs.Cart.Requests
{
    public class AddToCartRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
