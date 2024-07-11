namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class AttributeValue
    {
        [Key]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string Id { get; set; }

        public Guid SourceId { get; set; }

        [ForeignKey("SourceId")]
        public AttributeSource Source { get; set; }

        public virtual ICollection<AttributeValueLanguage> AttributeValueLanguages { get; set; }
    }
}
