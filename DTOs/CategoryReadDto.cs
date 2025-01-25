namespace asp_net_ecommerce_web_api.DTOs
{
    public class CategoryReadDto
    {
        public Guid CategoryId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}