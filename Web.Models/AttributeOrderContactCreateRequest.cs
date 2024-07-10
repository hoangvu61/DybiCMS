using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class AttributeOrderContactCreateRequest
    {
        public string AttributeId { get; set; }

        public int Order { get; set; }
    }
}
