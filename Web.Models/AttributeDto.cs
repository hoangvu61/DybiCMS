using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class AttributeDto
    {
        public string Id { get; set; }

        public Guid? SourceId { get; set; }

        public string Type { get; set; }

        public Dictionary<string, string> SourceNames { get; set; }
        public Dictionary<string, string> Titles { get; set; }
    }
}
