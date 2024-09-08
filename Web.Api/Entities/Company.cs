namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public partial class Company
    {

        [Key]
        public Guid Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Type { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string? Image { get; set; }

        [MaxLength(20)]
        [AllowNull]
        [Column(TypeName = "VARCHAR")]
        public string? TaxCode { get; set; }

        [MaxLength(200)]
        [AllowNull]
        public string? TaxCodePlace { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [AllowNull]
        public DateTime? PublishDate { get; set; }

        public WebConfig WebConfig { get; set; }

        public virtual ICollection<CompanyDomain> CompanyDomains { get; set; }
        public virtual ICollection<CompanyDetail> CompanyDetails { get; set; }
        public virtual ICollection<CompanyBranch> CompanyAddresses { get; set; }
        public virtual ICollection<CompanyLanguage> CompanyLanguages { get; set; }
    }
}
