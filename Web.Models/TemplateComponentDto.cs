using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class TemplateComponentDto
    {
        [MaxLength(5, ErrorMessage = "Template không thể dài hơn 5 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên template")]
        public string TemplateName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên Component")]
        [MaxLength(50, ErrorMessage = "Component không thể dài hơn 50 ký tự")]
        public string ComponentName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        [MaxLength(250, ErrorMessage = "Mô tả không thể dài hơn 250 ký tự")]
        public string Describe { get; set; }
    }
}
