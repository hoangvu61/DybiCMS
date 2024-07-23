using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseOutputProduct
    {
        [Description("Mã xuất kho")]
        public Guid OutputId { get; set; }

        [ForeignKey("OutputId")]
        public WarehouseOutput Output { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        [DefaultValue(0)]
        public int Quantity { get; set; }

        public virtual ICollection<WarehouseOutputProductCode> Codes { get; set; }
        public virtual ICollection<WarehouseOutputProductDetail> Details { get; set; }
    }
}
