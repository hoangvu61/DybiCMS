using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public class WarehouseInputRequest
    {

        [Key]
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }

        [AllowNull]
        public string? InputCode { get; set; }

        public int Type { get; set; }

        [AllowNull]
        public Guid? FromId { get; set; }

        public decimal TotalPrice { get; set; }

        [AllowNull]
        public string? Note { get; set; }

        public decimal Payment { get; set; }

        [AllowNull]
        public DateTime? DebitExpire { get; set; }
    }
}
