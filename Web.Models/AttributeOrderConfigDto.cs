using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class AttributeOrderConfigDto
    {
        public string Id { get; set; }

        public Guid? SourceId { get; set; }

        public string Type { get; set; }

        public int Priority { get; set; }

        public Dictionary<string, string> SourceNames { get; set; }
        public string Title { get; set; }
    }
}
