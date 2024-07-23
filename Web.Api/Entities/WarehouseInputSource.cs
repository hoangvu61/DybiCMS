using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class WarehouseInputSource
    {
        [Description("Mã nhập kho")]
        public Guid WarehouseInputId { get; set; }

        [ForeignKey("WarehouseId")]
        public WarehouseInput WarehouseInput { get; set; }

        [Description("Mã nhà cung cấp")]
        public Guid SourceId { get; set; }

        [ForeignKey("SourceId")]
        public WarehouseSource Source { get; set; }

        [Required]
        [MaxLength(150)]
        public string SourceName { get; set; }

        [AllowNull]
        [MaxLength(30)]
        public string? SourcePhone { get; set; }

        [AllowNull]
        [MaxLength(100)]
        public string? SourceEmail { get; set; }

        [AllowNull]
        [MaxLength(300)]
        public string? SourceAddress { get; set; }
    }
}
