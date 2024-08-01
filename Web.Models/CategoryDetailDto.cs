using System.ComponentModel.DataAnnotations;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class CategoryDetailDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại danh mục")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Trang danh sách")]
        public string ComponentList { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Trang chi tiết")]
        public string ComponentDetail { get; set; }

        public Guid? ParentId { get; set; }

        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [MaxLength(5, ErrorMessage = "Mã ngôn ngữ không thể dài hơn 5 ký tự")]
        public string LanguageCode { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(300, ErrorMessage = "Tiêu đề không thể dài hơn 300 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Brief { get; set; }

        public string? Content { get; set; }

        public int View { get; set; }

        public int Order { get; set; }

        public bool IsPuslished { get; set; }

        public DateTime CreateDate { get; set; }

        public FileData? Image { get; set; }
    }
}
