using System.ComponentModel.DataAnnotations;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ArticleCreateRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngôn ngữ")]
        public string LanguageCode { get; set; }

        public int Order { get; set; } = 50;

        public DateTime DisplayDate { get; set; } = DateTime.Now;

        public FileData? Image { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(300, ErrorMessage = "Tiêu đề không thể dài hơn 300 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Brief { get; set; }

        public string? Content { get; set; }

        public string? HTML { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
        public List<Guid> Relateds { get; set; } = new List<Guid>();
    }
}
