using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class WarehouseOutputRequest
    {

        [Key]
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }

        public int Type { get; set; }
        public Guid ToId { get; set; }
        
        public string? Note { get; set; }
    }
}
