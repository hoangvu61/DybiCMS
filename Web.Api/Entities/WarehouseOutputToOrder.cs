using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseOutputToOrder
    {
        [Description("Mã xuất kho")]
        public Guid OutputId { get; set; }

        [ForeignKey("OutputId")]
        public WarehouseOutput Output { get; set; }

        [Description("Mã đơn hàng")]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
