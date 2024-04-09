using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TitleLanguageDto
    {
        public string LanguageCode { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được rỗng")]
        public string Title { get; set; }
    }
}
