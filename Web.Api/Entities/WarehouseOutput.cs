using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class WarehouseOutput
    {

        [Key]
        public Guid Id { get; set; }

        public virtual ICollection<WarehouseInputProduct> Products { get; set; }

        [Required]
        [MaxLength(200)]
        public string ToName { get; set; }

        [AllowNull]
        [MaxLength(30)]
        public string? ToPhone { get; set; }

        [Required]
        [MaxLength(300)]
        public string ToAddress { get; set; }

        [Description("Loại: Xuất bán hàng / Xuất sản xuất / Xuất chuyển kho")]
        [Required]
        [DefaultValue(0)]
        public int Type { get; set; }

        [AllowNull]
        public string? Note { get; set; }
    }
}
