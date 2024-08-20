using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public class WarehouseOutputRequest
    {

        [Key]
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }

        public int Type { get; set; }

        [AllowNull]
        public Guid? ToId { get; set; }

        [AllowNull]
        public string? Note { get; set; }
    }
}
