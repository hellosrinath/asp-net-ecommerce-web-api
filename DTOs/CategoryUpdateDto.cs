using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_ecommerce_web_api.DTOs
{
    public class CategoryUpdateDto
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 100 characters")]
        public string Name { get; set; } = string.Empty;
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Category description must between 3 and 500 characters")]
        public string Description { get; set; } = string.Empty;
    }
}