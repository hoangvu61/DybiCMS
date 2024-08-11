using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class WarehouseOutputToWarehouse
    {
        [Key]
        [Description("Mã xuất kho")]
        public Guid OutputId { get; set; }

        [ForeignKey("OutputId")]
        public WarehouseOutput Output { get; set; }

        [Description("Mã kho, chuyển đến kho này")]
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
