namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class CompanyBranch
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [MaxLength(5)]
        [Required]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }

        [MaxLength(250)]
        [AllowNull]
        public string? Name { get; set; }

        [MaxLength(250)]
        [Required]
        public string? Address { get; set; }

        [MaxLength(100)]
        [AllowNull]
        public string? Email { get; set; }

        [MaxLength(15)]
        [AllowNull]
        [Column(TypeName = "VARCHAR")]
        public string? Phone { get; set; }

        public int Order { get; set; }
    }
}
