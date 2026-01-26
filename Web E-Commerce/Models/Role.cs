namespace Web_E_Commerce.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}