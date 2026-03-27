namespace Web_E_Commerce.Models
{
    public class Address : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public string RecipientName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Ward {  get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
    }
}
