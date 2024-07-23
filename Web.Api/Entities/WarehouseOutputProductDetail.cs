using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseOutputProductDetail
    {
        [Description("Mã xuất kho")]
        public Guid OutputId { get; set; }

        [ForeignKey("OutputId")]
        public WarehouseOutput Output { get; set; }

        [Description("Mã nhập kho")]
        public Guid InputId { get; set; }

        [ForeignKey("InputId")]
        public WarehouseInput Input { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        [Description("Gía đơn vị, giá vốn xuất kho")]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [DefaultValue(0)]
        public int Quantity { get; set; }
    }
}
