using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class DebtCustomer
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid CustomerId { get; set; }

        public decimal TotalDebt { get; set; }

        public decimal Debt { get; set; }
        public DateTime CreateDate { get; set; }

        [Description("1: Khách nợ; 2: Khách trả nợ")]
        [DefaultValue("0")]
        public int Type { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
