namespace Web_E_Commerce.Models
{
    public class CartItem : BaseEntity
    {
        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = default!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal PriceAtTime { get; set; } // snapshot of product price when added to cart
    }
}