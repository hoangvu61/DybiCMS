using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public partial class WarehouseSupplierDto
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [AllowNull]
        [MaxLength(30)]
        public string? Phone { get; set; }

        [AllowNull]
        [MaxLength(100)]
        public string? Email { get; set; }

        [AllowNull]
        [MaxLength(300)]
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;

        [AllowNull]
        [MaxLength(300)]
        public string? Note { get; set; }
    }
}
