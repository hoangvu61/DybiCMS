using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseInputFromOrder
    {
        [Description("Mã nhập kho")]
        public Guid WarehouseInputId { get; set; }

        [ForeignKey("WarehouseInputId")]
        public WarehouseInput Input { get; set; }

        [Description("Mã đơn hàng")]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
