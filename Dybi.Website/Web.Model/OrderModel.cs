
namespace Web.Model
{
    using System;

    public partial class OrderModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? CancelDate { get; set; }
    }
}  
