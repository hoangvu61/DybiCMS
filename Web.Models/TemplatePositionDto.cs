using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class TemplatePositionDto
    {
        [MaxLength(5, ErrorMessage = "Template không thể dài hơn 5 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên Template")]
        public string TemplateName { get; set; }

        [MaxLength(50, ErrorMessage = "Component không thể dài hơn 50 ký tự")]
        public string ComponentName { get; set; }

        [MaxLength(50, ErrorMessage = "Position không thể dài hơn 50 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên Position")]
        public string PositionName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        [MaxLength(250, ErrorMessage = "Mô tả không thể dài hơn 250 ký tự")]
        public string Describe { get; set; }
    }
}
