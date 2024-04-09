using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class CompanyDomainDto
    {
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Domain")]
        [MaxLength(50, ErrorMessage = "Domain không thể dài hơn 50 ký tự")]
        public string Domain { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngôn ngữ")]
        public string LanguageCode { get; set; }
    }
}
