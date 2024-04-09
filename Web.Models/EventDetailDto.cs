using System.ComponentModel.DataAnnotations;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class EventDetailDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [MaxLength(5, ErrorMessage = "Mã ngôn ngữ không thể dài hơn 5 ký tự")]
        public string LanguageCode { get; set; } = "vi";

        public int Order { get; set; } = 50;

        public bool IsPuslished { get; set; } = true;

        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Địa điểm không được để trống")]
        [MaxLength(300, ErrorMessage = "Địa điểm không thể dài hơn 300 ký tự")]
        public string Place { get; set; }

        public FileData? Image { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(300, ErrorMessage = "Tiêu đề không thể dài hơn 300 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Brief { get; set; }

        public string? Content { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int View { get; set; } = 0;
    }
}
