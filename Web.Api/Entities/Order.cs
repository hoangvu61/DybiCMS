using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class Order
    {

        [Key]
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(30)]
        public string CustomerPhone { get; set; }

        [AllowNull]
        [MaxLength(300)]
        public string? CustomerAddress { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [AllowNull]
        public DateTime? ConfirmDate { get; set; }

        [AllowNull]
        public DateTime? SendDate { get; set; }

        [AllowNull]
        public DateTime? ReceiveDate { get; set; }

        [AllowNull]
        public DateTime? CancelDate { get; set; }

        [AllowNull]
        [MaxLength(300)]
        public string? Note { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }

        public virtual OrderDelivery? Delivery { get; set; }
        public virtual OrderDebt? Debt { get; set; }
    }
}
