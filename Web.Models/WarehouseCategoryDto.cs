using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public partial class WarehouseCategoryDto
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [MaxLength(300, ErrorMessage = "Tiêu đề không thể dài hơn 300 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Trang chi tiết")]
        public string Describe { get; set; }
        public bool IsPuslished { get; set; }

        [AllowNull]
        public string? ComponentList { get; set; }

        [AllowNull]
        public string? ComponentDetail { get; set; }
    }
}
