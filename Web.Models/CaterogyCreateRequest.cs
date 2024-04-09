using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CaterogyCreateRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại danh mục")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Trang danh sách")]
        public string ComponentList { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Trang chi tiết")]
        public string ComponentDetail { get; set; }

        [Required(ErrorMessage = "Mã ngôn ngữ không được để trống")]
        [MaxLength(5, ErrorMessage = "Mã ngôn ngữ không thể dài hơn 5 ký tự")]
        public string LanguageCode { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [MaxLength(300, ErrorMessage = "Tiêu đề không thể dài hơn 300 ký tự")]
        public string Title { get; set; }

        public Guid? ParentId { get; set; }
    }
}
