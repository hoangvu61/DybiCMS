
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public class OrderDeliveryRequest
    {
        public int DeliveryId { get; set; }

        [AllowNull]
        public string? DeliveryName { get; set; }

        [AllowNull]
        public string? DeliveryCode { get; set; }

        public decimal DeliveryFee { get; set; }
        public bool COD { get; set; }

        [AllowNull]
        public string? DeliveryNote { get; set; }   
    }
}
