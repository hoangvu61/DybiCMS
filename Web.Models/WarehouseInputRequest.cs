
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public class WarehouseInputRequest
    {

        [Key]
        public Guid Id { get; set; }

        [AllowNull]
        public string? DeliveryName { get; set; }
        public string DeliveryCode { get; set; }
        public decimal DeliveryFee { get; set; }
        public bool COD { get; set; }

        [AllowNull]
        public string? DeliveryNote { get; set; }   
    }
}
