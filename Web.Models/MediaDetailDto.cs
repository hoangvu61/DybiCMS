using System.ComponentModel.DataAnnotations;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class MediaDetailDto
    {
        public MediaDetailDto(string type = "") 
        {
            if (!string.IsNullOrEmpty(type))
                Type = type;
        }
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [MaxLength(5, ErrorMessage = "Mã ngôn ngữ không thể dài hơn 5 ký tự")]
        public string LanguageCode { get; set; } = "vi";

        public FileData? Image { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(300, ErrorMessage = "Tiêu đề không thể dài hơn 300 ký tự")]
        public string Title { get; set; }

        public string? Brief { get; set; }

        public string? Content { get; set; }

        public int Order { get; set; } = 50;

        public bool IsPublished { get; set; } = true;

        public string? Url { get; set; }

        public string? Target { get; set; } = "_self";

        public string? Embed { get; set; }

        public string Type { get; set; }
    }
}
