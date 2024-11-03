namespace Web.Models
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public decimal Discount { get; set; }
        public int DiscountType { get; set; }
        public string? Note { get; set; }
    }
}
