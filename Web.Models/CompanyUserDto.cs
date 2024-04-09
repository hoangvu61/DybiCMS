using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public class CompanyUserDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(250, ErrorMessage = "Họ không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        public string FirstName { get; set; }

        [MaxLength(250, ErrorMessage = "Tên không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string LastName { get; set; }

        [MaxLength(250, ErrorMessage = "Tên đăng nhập không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string UserName { get; set; }

        [AllowNull]
        public string? Password { get; set; }

        [MaxLength(100, ErrorMessage = "Email không thể dài hơn 100 ký tự")]
        public string Email { get; set; }

        [MaxLength(15, ErrorMessage = "Số điện thoại không thể dài hơn 15 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập số điện tho")]
        public string Phone { get; set; }

        public bool ProductManage { get; set; } = false;
        public bool DocumentManage { get; set; } = false;
        public bool VideoManage { get; set; } = false;
        public bool PictureManage { get; set; } = false;
        public bool AudioManage { get; set; } = false;
        public bool EventManage { get; set; } = false;
    }
}
