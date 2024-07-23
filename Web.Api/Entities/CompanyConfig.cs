using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class CompanyConfig
    {
        [Required]
        [MaxLength(50)]
        public string Key { get; set; }
        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [MaxLength(50)]
        public string Value { get; set; }
    }
}
