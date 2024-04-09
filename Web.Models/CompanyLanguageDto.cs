using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public class CompanyLanguageDto
    {
        [Required(ErrorMessage = "Mã công ty không được để trống")]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [MaxLength(5, ErrorMessage = "Mã ngôn ngữ không thể dài hơn 5 ký tự")]
        public string LanguageCode { get; set; }
    }
}
