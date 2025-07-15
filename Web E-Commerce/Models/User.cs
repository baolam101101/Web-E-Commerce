using Web_E_Commerce.Enums;

namespace Web_E_Commerce.Models
{
    public class User
    {
        public int Id { get; set; }
        // Login
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Personal info
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        
        public ICollection<UserRole> UserRoles { get; set; } = [];
        public ICollection<Order>? Orders { get; set; } = [];
    }
}