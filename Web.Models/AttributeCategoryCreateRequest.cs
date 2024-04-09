using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class AttributeCategoryCreateRequest
    {
        public string AttributeId { get; set; }

        public Guid CategoryId { get; set; }

        public int Order { get; set; }
    }
}
