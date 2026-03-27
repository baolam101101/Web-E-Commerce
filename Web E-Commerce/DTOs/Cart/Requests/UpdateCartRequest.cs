namespace Web_E_Commerce.DTOs.Cart.Requests
{
    public class UpdateCartRequest
    {
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
