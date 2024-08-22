using System.ComponentModel;

namespace Web.Models
{
    public partial class DebtDto
    {
        public Guid Id { get; set; }

        public Guid DebtorOrCreditor_Id { get; set; }
        public string DebtorOrCreditor_Name { get; set; }
        public string DebtorOrCreditor_Phone { get; set; }
        public string DebtorOrCreditor_Address { get; set; }

        public decimal TotalDebt { get; set; }

        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }

        [Description("1: Mình nợ; 2: Mình trả nợ")]
        public int Type { get; set; }
    }
}
