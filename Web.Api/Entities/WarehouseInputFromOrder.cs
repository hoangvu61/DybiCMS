using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseInputFromOrder
    {
        [Key]
        [Description("Mã nhập kho")]
        public Guid InputId { get; set; }

        [ForeignKey("InputId")]
        public WarehouseInput Input { get; set; }

        [Description("Mã đơn hàng")]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
