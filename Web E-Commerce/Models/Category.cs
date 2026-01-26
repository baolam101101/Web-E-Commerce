using System.ComponentModel.DataAnnotations;

namespace Web_E_Commerce.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
