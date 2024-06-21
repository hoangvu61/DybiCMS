using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class OrderDelivery
    {
        [Key]
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int DeliveryId { get; set; }

        [Required]
        public string DeliveryCode { get; set; }

        public decimal DeliveryFee { get; set; }
        public bool COD { get; set; }

        [AllowNull]
        public string? DeliveryNote { get; set; }
    }
}
