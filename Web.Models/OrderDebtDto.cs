namespace Web.Models
{
    public class OrderDebtDto
    {
        public decimal Debit { get; set; } = 0;
        public DateTime? DebitExpire { get; set; }
    }
}
