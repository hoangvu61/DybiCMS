using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class AttributeDetailDto
    {   
        public string Id { get; set; }

        public Guid? SourceId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại thuộc tính")]
        [MaxLength(10, ErrorMessage = "Loại tuộc tính không thể dài hơn 10 ký tự")]
        public string Type { get; set; } = "Text";

        public List<TitleLanguageDto> Titles { get; set; } = new List<TitleLanguageDto>();
    }
}
