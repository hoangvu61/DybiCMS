using System.ComponentModel.DataAnnotations;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class EventCreateRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngôn ngữ")]
        public string LanguageCode { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Địa điểm không được để trống")]
        [MaxLength(300, ErrorMessage = "Địa điểm không thể dài hơn 300 ký tự")]
        public string Place { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(300, ErrorMessage = "Tiêu đề không thể dài hơn 300 ký tự")]
        public string Title { get; set; }
    }
}
