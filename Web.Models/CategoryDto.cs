using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class CategoryDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại danh mục")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Trang danh sách")]
        public string ComponentList { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Trang chi tiết")]
        public string ComponentDetail { get; set; }

        public Guid? ParentId { get; set; }
        public Dictionary<string, string> Titles { get; set; }

        public int Order { get; set; }

        public bool IsPuslished { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
