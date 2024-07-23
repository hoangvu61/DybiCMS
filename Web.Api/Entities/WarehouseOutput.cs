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

        [Description("Mã kho")]
        public Guid WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        public DateTime CreateDate { get; set; }

        [Description("Loại: Xuất bán hàng / Xuất sản xuất / Xuất chuyển kho")]
        [Required]
        [DefaultValue(0)]
        public int Type { get; set; }

        [AllowNull]
        public string? Note { get; set; }

        [Description("Xuất bán hàng phải có đơn hàng trước rồi mới xuất kho")]
        public WarehouseOutputToOrder? Order { get; set; }

        [Description("Xuất sản xuất")]
        public WarehouseOutputToFactory? Factory { get; set; }

        public virtual ICollection<WarehouseOutputProduct> Products { get; set; }
    }
}
