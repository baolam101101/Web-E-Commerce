using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        public UserRole Role { get; set; } = UserRole.User;

        public ICollection<Order>? Orders { get; set; } = [];
    }
}
