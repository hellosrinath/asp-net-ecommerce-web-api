using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_ecommerce_web_api.DTOs
{
    public class CategoryUpdateDto
    {
        public required string Name { get; set; }
        public string Description { get; set; } = "";
    }
}