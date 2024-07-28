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

        public decimal TotalPrice { get; set; }
        
        public string? Note { get; set; }

        public Guid Payment { get; set; }
    }
}
