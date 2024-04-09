using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class TemplateSkinDto
    {
        [MaxLength(5, ErrorMessage = "Template không thể dài hơn 5 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên template")]
        public string TemplateName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên Skin")]
        [MaxLength(50, ErrorMessage = "Skin không thể dài hơn 50 ký tự")]
        public string SkinName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên Module")]
        [MaxLength(50, ErrorMessage = "Module không thể dài hơn 50 ký tự")]
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        [MaxLength(250, ErrorMessage = "Mô tả không thể dài hơn 250 ký tự")]
        public string Describe { get; set; }
    }
}
