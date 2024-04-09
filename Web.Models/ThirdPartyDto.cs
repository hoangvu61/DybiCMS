using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class ThirdPartyDto
    {
        public Guid Id { get; set; }

        [MaxLength(50, ErrorMessage = "ThirdPartyName không thể dài hơn 50 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên ThirdPartyName")]
        public string ThirdPartyName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string ContentHTML { get; set; }

        [MaxLength(50, ErrorMessage = "ComponentName không thể dài hơn 50 ký tự")]
        public string ComponentName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vị trí")]
        [MaxLength(50, ErrorMessage = "PositionName không thể dài hơn 50 ký tự")]
        public string PositionName { get; set; }

        public bool Apply { get; set; } = true;
    }
}
