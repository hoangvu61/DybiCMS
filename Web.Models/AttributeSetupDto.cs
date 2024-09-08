using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class AttributeSetupDto
    {
        public string Id { get; set; }

        public Guid? SourceId { get; set; }
        public string Type { get; set; }

        public string? Value { get; set; }
        public string Title { get; set; }

        public FileData? Image { get; set; }
        public List<TitleStringDto>? Values { get; set; }
    }
}
