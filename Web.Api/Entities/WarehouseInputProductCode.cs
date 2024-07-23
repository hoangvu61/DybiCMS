using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseInputProductCode
    {
        [Description("Mã seri từng sản phẩm")]
        [Required]
        [MaxLength(100)]
        public string ProductCode { get; set; }

        [Description("Mã nhập kho")]
        public Guid InputId { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("InputId,ProductId")]
        public WarehouseInputProduct Product { get; set; }
    }
}
