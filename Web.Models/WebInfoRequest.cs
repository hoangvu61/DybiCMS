using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Models.SeedWork;

namespace Web.Models
{
    public class WebInfoRequest
    {
        [Required(ErrorMessage = "Vui lòng chọn ngôn ngữ")]
        public string LanguageCode { get; set; }

        [MaxLength(20, ErrorMessage = "Mã số thuế không thể dài hơn 20 ký tự")]
        public string? TaxCode { get; set; }

        public DateTime? PublishDate { get; set; }

        [MaxLength(250, ErrorMessage = "Tên đầy đủ công ty không thể dài hơn 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên đầy đủ của công ty")]
        public string FullName { get; set; }

        [MaxLength(150, ErrorMessage = "Tên hiển thị công ty không thể dài hơn 150 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên hiển thị của công ty")]
        public string DisplayName { get; set; }

        [MaxLength(150, ErrorMessage = "Biêt danh không thể dài hơn 150 ký tự")]
        public string? NickName { get; set; }

        [MaxLength(150, ErrorMessage = "Biêt danh không thể dài hơn 150 ký tự")]
        public string? JobTitle { get; set; }

        [MaxLength(250, ErrorMessage = "Slogan không thể dài hơn 250 ký tự")]
        public string? Slogan { get; set; }
        public string? Vision { get; set; }
        public string? Mission { get; set; }
        public string? CoreValues { get; set; }
        public string? Motto { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả ngắn")]
        public string Brief { get; set; }

        public string? AboutUs { get; set; }

        public string Type { get; set; }
    }
}
