using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }

        public bool IsPeristant { get; set; }
    }
}
