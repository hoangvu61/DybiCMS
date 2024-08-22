namespace Web.Models
{
    public partial class DebtRequest
    {
        public Guid Id { get; set; }

        public Guid DebtorOrCreditor_Id { get; set; }
        public decimal Repayment { get; set; }
    }
}
