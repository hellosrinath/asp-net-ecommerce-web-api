namespace asp_net_ecommerce_web_api.DTOs
{
    public class CategoryCreateDto
    {
        public required string Name { get; set; }
        public string Description { get; set; } = "";
    }
}