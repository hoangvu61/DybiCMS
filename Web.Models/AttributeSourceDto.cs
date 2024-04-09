using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class AttributeSourceDto
    {
        public Guid Id { get; set; }
        public List<TitleLanguageDto> Titles { get; set; } = new List<TitleLanguageDto>();
    }
}
