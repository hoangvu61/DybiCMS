using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class WarehouseFactory
    {

        [Key]
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

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

        [AllowNull]
        [MaxLength(300)]
        public string? Note { get; set; }


    }
}
