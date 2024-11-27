using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public class MyUserChangePasswordRequest
    {

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại")]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        public string? NewPassword { get; set; }
    }
}
