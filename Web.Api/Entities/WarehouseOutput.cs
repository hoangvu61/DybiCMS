using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class WarehouseOutput
    {

        [Key]
        public Guid Id { get; set; }

        [Description("Loại: Xuất bán hàng / Xuất sản xuất / Xuất chuyển kho")]
        [Required]
        [DefaultValue(0)]
        public int Type { get; set; }

        [AllowNull]
        public string? Note { get; set; }

        [Description("Xuất bán hàng phải có đơn hàng trước rồi mới xuất kho")]
        public WarehouseOutputToOrder? Order { get; set; }

        public virtual ICollection<WarehouseOutputProduct> Products { get; set; }
    }
}
