using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class MenuCreateRequest
    {
        [Required(ErrorMessage = "Vui lòng chọn")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trang đến")]
        public string Component { get; set; }
        public int Order { get; set; } = 1;
    }
}
