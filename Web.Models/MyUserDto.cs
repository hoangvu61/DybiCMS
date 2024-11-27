using System.ComponentModel.DataAnnotations;
namespace Web.Models
{
    public class MyUserDto
    {
        [MaxLength(250, ErrorMessage = "Họ không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        public string FirstName { get; set; }

        [MaxLength(250, ErrorMessage = "Tên không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string LastName { get; set; }

        [MaxLength(250, ErrorMessage = "Tên đăng nhập không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string UserName { get; set; }

        [MaxLength(100, ErrorMessage = "Email không thể dài hơn 100 ký tự")]
        public string Email { get; set; }

        [MaxLength(15, ErrorMessage = "Số điện thoại không thể dài hơn 15 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; }
    }
}
