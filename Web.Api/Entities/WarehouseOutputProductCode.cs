using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseOutputProductCode
    {
        [Description("Mã seri từng sản phẩm")]
        [Required]
        [MaxLength(100)]
        public string ProductCode { get; set; }

        [Description("Mã xuất kho")]
        public Guid OutputId { get; set; }

        [Description("Mã nhập kho")]
        public Guid InputId { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("InputId")]
        public WarehouseInput Input { get; set; }

        [ForeignKey("OutputId")]
        public WarehouseOutput Output { get; set; }

        [ForeignKey("OutputId,ProductId")]
        public WarehouseOutputProduct OutProduct { get; set; }

        [ForeignKey("InputId,ProductId,ProductCode")]
        public WarehouseInputProductCode ProductInputCode { get; set; }
    }
}
