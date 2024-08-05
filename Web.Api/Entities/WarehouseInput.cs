using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class WarehouseInput
    {

        [Key]
        public Guid Id { get; set; }

        [Description("Mã kho")]
        public Guid WarehouseId { get; set; }
        
        public DateTime CreateDate { get; set; }

        [AllowNull]
        [MaxLength(50)]
        [Description("Mã lô hàng nhập")]
        public string? InputCode { get; set; }

        [Description("Tổng phí nhập hàng")]
        [DefaultValue(0)]
        public decimal TotalPrice { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Type { get; set; }

        [AllowNull]
        public string? Note { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        public WarehouseInputDebt? Debt { get; set; }
        public WarehouseInputFromSupplier? FromSupplier { get; set; }
        public WarehouseInputFromFactory? FromFactory { get; set; }
        public WarehouseInputFromWarehouse? FromWarehouse { get; set; }

        [Description("Xuất bán hàng phải có đơn hàng trước rồi mới xuất kho")]
        public WarehouseInputFromOrder? FromOrder { get; set; }
        public virtual ICollection<WarehouseInputProduct> Products { get; set; }
    }
}
