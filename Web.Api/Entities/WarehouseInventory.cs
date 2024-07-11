using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseInventory
    {
        [Description("Mã nhà cung cấp")]
        public Guid WarehouseId { get; set; }
        
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        [DefaultValue(0)]
        public int InventoryNumber { get; set; }

        
    }
}
