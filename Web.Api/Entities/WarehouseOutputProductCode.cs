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

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        [Description("Mã xuất kho")]
        public Guid OutputId { get; set; }


        [ForeignKey("OutputId")]
        public WarehouseOutput Output { get; set; }
    }
}
