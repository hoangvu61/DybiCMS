using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class WarehouseConfig
    {
        [MaxLength(50)]
        public string Key { get; set; }

        [ForeignKey("Key")]
        public Config Config { get; set; }

        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [AllowNull]
        public string? Value { get; set; }

    }
}
