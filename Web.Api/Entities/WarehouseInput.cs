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

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        [Description("Mã nhà cung cấp")]
        public Guid SourceId { get; set; }

        [ForeignKey("SourceId")]
        public WarehouseSource Source { get; set; }

        public DateTime CreateDate { get; set; }

        [Description("Tổng phí nhập hàng")]
        [DefaultValue(0)]
        public decimal TotalPrice { get; set; }

        [AllowNull]
        public string? Note { get; set; }

        public virtual ICollection<WarehouseInputProduct> Products { get; set; }
    }
}
