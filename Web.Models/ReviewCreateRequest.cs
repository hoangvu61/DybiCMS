using System.ComponentModel.DataAnnotations;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ReviewCreateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ReviewFor { get; set; }
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }
    }
}
