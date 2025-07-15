using System.ComponentModel.DataAnnotations;

namespace Web_E_Commerce.DTOs.Category.Requests
{
    public class CategoryUpdateRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
