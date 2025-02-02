namespace asp_net_ecommerce_web_api.models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }


        public Category()
        {
            CategoryId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}