using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{    
    public partial class ModuleParamDto
    {
        [MaxLength(50, ErrorMessage = "Module không thể dài hơn 50 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên module")]
        public string ParamName { get; set; }

        [MaxLength(50, ErrorMessage = "Module không thể dài hơn 50 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên module")]
        public string ModuleName { get; set; }

        [MaxLength(250, ErrorMessage = "Họ không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Describe { get; set; }

        public string? DefaultValue { get; set; }

        [MaxLength(20, ErrorMessage = "Kiểu tham số không thể dài hơn 10 ký tự")]
        [Required(ErrorMessage = "Vui lòng chọn loại giá trị")]
        public string Type { get; set; }
    }
}
