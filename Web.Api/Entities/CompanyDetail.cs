
namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class CompanyDetail
    {
        [Required]
        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }

        [MaxLength(250)]
        [Required]
        public string FullName { get; set; }

        [MaxLength(150)]
        [AllowNull]
        public string? NickName { get; set; }

        [MaxLength(150)]
        [Required]
        public string DisplayName { get; set; }

        [MaxLength(150)]
        [AllowNull]
        public string? JobTitle { get; set; }

        [MaxLength(250)]
        [AllowNull]
        public string? Slogan { get; set; }

        [AllowNull]
        public string? Motto { get; set; }

        [AllowNull]
        public string? Vision { get; set; }

        [AllowNull]
        public string? Mission { get; set; }

        [AllowNull]
        public string? CoreValues { get; set; }

        [Required]
        public string Brief { get; set; }

        [AllowNull]
        public string? AboutUs { get; set; }
    }
}
