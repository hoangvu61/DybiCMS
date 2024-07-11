using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseInputProduct
    {
        public Guid WarehouseId { get; set; }

        [Description("Mã nhà cung cấp")]
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        public DateTime CreateDate { get; set; }

        [Description("Gía đơn vị, giá nhập")]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [DefaultValue(0)]
        public int Quantity { get; set; }

        
    }
}
