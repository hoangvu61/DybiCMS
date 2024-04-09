namespace Web.Api.Entities
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class ThirdParty
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [Required]
        [MaxLength(50)]
        public string ThirdPartyName { get; set; }

        [Required]
        public string ContentHTML { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string ComponentName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string PositionName { get; set; }

        [Required]
        [DefaultValue("true")]
        public bool Apply { get; set; }
    }
}
