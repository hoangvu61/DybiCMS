using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class ModuleDto
    {
        [MaxLength(50, ErrorMessage = "Module không thể dài hơn 50 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên module")]
        public string ModuleName { get; set; }

        [MaxLength(250, ErrorMessage = "Mô tả không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Describe { get; set; }
    }
}
