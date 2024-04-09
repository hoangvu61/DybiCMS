using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ProductDto
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public Dictionary<string, string> CategoryNames { get; set; }
        public Dictionary<string, string> Titles { get; set; }

        public int Order { get; set; }

        public bool IsPublished { get; set; }

        public FileData? Image { get; set; }

        public int View { get; set; }

        public string? Code { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public decimal DiscountType { get; set; } = 0;
    }
}
