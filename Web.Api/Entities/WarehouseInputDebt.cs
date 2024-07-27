using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseInputDebt
    {

        [Key]
        public Guid InputId { get; set; }

        [Description("Nợ người bán")]
        [DefaultValue(0)]
        public decimal Debit { get; set; }

        [Description("Thời hạn thanh toán")]
        public DateTime? DebitExpire { get; set; }

        [ForeignKey("InputId")]
        public WarehouseInput Input { get; set; }
    }
}
