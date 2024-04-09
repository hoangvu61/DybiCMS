using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ModuleConfigCreateRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn module")]
        public string SkinName { get; set; }
        public bool OnTemplate { get; set; } = true;

        public string? ComponentName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vị trí")]
        public string Position { get; set; }

        public int Order { get; set; } = 50;
    }
}
