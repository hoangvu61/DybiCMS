using System.ComponentModel.DataAnnotations;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class TemplateDto
    {
        [MaxLength(5, ErrorMessage = "Template không thể dài hơn 5 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên template")]
        public string TemplateName { get; set; }

        public FileData? Image { get; set; }

        public bool IsPublic { get; set; } = true;
        public bool IsPublished { get; set; } = true;
    }
}
