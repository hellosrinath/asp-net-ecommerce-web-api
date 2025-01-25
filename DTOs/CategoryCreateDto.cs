using System.ComponentModel.DataAnnotations;

namespace asp_net_ecommerce_web_api.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Category description can not exceed 500 characters")]
        public string Description { get; set; } = "";
    }
}