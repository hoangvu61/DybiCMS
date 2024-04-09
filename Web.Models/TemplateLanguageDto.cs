using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class TemplateLanguageDto
    {
        [MaxLength(5, ErrorMessage = "Template không thể dài hơn 5 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên template")]
        public string TemplateName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên Key")]
        [MaxLength(50, ErrorMessage = "Key không thể dài hơn 50 ký tự")]
        public string LanguageKey { get; set; }
    }
}
