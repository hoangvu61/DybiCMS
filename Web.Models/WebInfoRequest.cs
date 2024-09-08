using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public class WebInfoRequest
    {
        [Required(ErrorMessage = "Vui lòng chọn ngôn ngữ")]
        public string LanguageCode { get; set; }

        [MaxLength(20, ErrorMessage = "Mã số thuế không thể dài hơn 20 ký tự")]
        [AllowNull]
        public string? TaxCode { get; set; }

        [MaxLength(200, ErrorMessage = "Nơi cấp không thể dài hơn 200 ký tự")]
        [AllowNull]
        public string? TaxCodePlace { get; set; }
        public DateTime? PublishDate { get; set; }

        [MaxLength(250, ErrorMessage = "Tên đầy đủ công ty không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên đầy đủ của công ty")]
        public string FullName { get; set; }

        [MaxLength(150, ErrorMessage = "Tên hiển thị công ty không thể dài hơn 150 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên hiển thị của công ty")]
        public string DisplayName { get; set; }

        [MaxLength(150, ErrorMessage = "Biêt danh không thể dài hơn 150 ký tự")]
        [AllowNull]
        public string? NickName { get; set; }

        [MaxLength(150, ErrorMessage = "Biêt danh không thể dài hơn 150 ký tự")]

        [AllowNull]
        public string? JobTitle { get; set; }

        [MaxLength(250, ErrorMessage = "Slogan không thể dài hơn 250 ký tự")]
        [AllowNull]
        public string? Slogan { get; set; }
        [AllowNull]
        public string? Vision { get; set; }
        [AllowNull]
        public string? Mission { get; set; }
        [AllowNull]
        public string? CoreValues { get; set; }
        [AllowNull]
        public string? Motto { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả ngắn")]
        public string Brief { get; set; }

        public string? AboutUs { get; set; }

        public string Type { get; set; }
    }
}
