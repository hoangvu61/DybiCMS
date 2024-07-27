using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class OrderDebt
    {

        [Key]
        public Guid OrderId { get; set; }

        [Description("Người mua nợ")]
        [DefaultValue(0)]
        public decimal Debit { get; set; }

        [Description("Thời hạn thanh toán")]
        public DateTime? DebitExpire { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
