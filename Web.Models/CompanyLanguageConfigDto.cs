using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public partial class CompanyLanguageConfigDto
    {
        [Required(ErrorMessage = "Vui lòng nhập tên Key")]
        [MaxLength(50, ErrorMessage = "Key không thể dài hơn 50 ký tự")]
        public string LanguageKey { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }


        [MaxLength(250)]
        public string Describe { get; set; }
    }
}
