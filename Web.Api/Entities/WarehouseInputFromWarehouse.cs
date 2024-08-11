using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class WarehouseInputFromWarehouse
    {
        [Key]
        [Description("Mã nhập kho")]
        public Guid InputId { get; set; }

        [ForeignKey("InputId")]
        public WarehouseInput WarehouseInput { get; set; }

        [Description("Mã kho, từ kho này chuyển đến")]
        public Guid WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        [Required]
        [MaxLength(150)]
        public string WarehouseName { get; set; }

        [AllowNull]
        [MaxLength(30)]
        public string? WarehousePhone { get; set; }

        [AllowNull]
        [MaxLength(100)]
        public string? WarehouseEmail { get; set; }

        [AllowNull]
        [MaxLength(300)]
        public string? WarehouseAddress { get; set; }
    }
}
