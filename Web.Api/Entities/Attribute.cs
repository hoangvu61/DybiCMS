namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class Attribute
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string Id { get; set; } 

        [Required]
        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [AllowNull]
        public Guid? SourceId { get; set; }
        [ForeignKey("SourceId")]
        public AttributeSource? Source { get; set; }

        [Required]
        [MaxLength(10)]
        [Column(TypeName = "VARCHAR")]
        public string Type { get; set; }

        public virtual ICollection<AttributeLanguage> AttributeLanguages { get; set; }
        public virtual ICollection<AttributeCategory> AttributeCategories { get; set; }
    }
}
