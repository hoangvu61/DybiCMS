using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class WarehouseProductDto
    {
        public Guid Id { get; set; }

        [AllowNull]
        public string? Code { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }

        [AllowNull]
        public string? CategoryName { get; set; }
        public FileData? Image { get; set; }

        public int CountSeries { get; set; }
    }
}
