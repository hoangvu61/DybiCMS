
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CompanyBranchDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngôn ngữ")]
        public string LanguageCode { get; set; }

        [MaxLength(250, ErrorMessage = "Tên chi nhánh không thể dài hơn 250 ký tự")]
        public string? Name { get; set; }

        [MaxLength(250, ErrorMessage = "Địa chỉ không thể dài hơn 250 ký tự")]
        public string? Address { get; set; }

        [MaxLength(100, ErrorMessage = "Email không thể dài hơn 100 ký tự")]
        public string? Email { get; set; }

        [MaxLength(15, ErrorMessage = "Số điện thoại không thể dài hơn 15 ký tự")]
        public string? Phone { get; set; }

        public int Order { get; set; }
    }
}
