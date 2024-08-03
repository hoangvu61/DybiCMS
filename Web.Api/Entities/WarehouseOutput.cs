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

        [Required]
        [DefaultValue(0)]
        public int Type { get; set; }

        [AllowNull]
        public string? Note { get; set; }

        [Description("Xuất bán hàng phải có đơn hàng trước rồi mới xuất kho")]
        public WarehouseOutputToOrder? ToOrder { get; set; }

        [Description("Xuất sản xuất")]
        public WarehouseOutputToFactory? ToFactory { get; set; }

        [Description("Xuất sản trả Nhà cung cấp")]
        public WarehouseOutputToSupplier? ToSupplier { get; set; }

        [Description("Xuất chuyển kho")]
        public WarehouseOutputToWarehouse? ToWarehouse { get; set; }

        public virtual ICollection<WarehouseOutputProduct> Products { get; set; }
    }
}
