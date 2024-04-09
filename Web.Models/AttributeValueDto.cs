using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class AttributeValueDto
    {
        public string Id { get; set; }

        public Guid SourceId { get; set; }
        public List<TitleLanguageDto> Titles { get; set; } = new List<TitleLanguageDto>();
    }
}
