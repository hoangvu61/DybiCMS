using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class AttributeCategoryDto
    {
        public string AttributeId { get; set; }

        public Guid CategoryId { get; set; }

        public int Order { get; set; }

        public Dictionary<string, string> CategoryNames { get; set; }
        public Dictionary<string, string> AttributeTitles { get; set; }
    }
}
