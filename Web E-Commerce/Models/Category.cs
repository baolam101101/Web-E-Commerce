using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Web_E_Commerce.Models
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = default!;
        [Required]
        public string NormalizedName { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
