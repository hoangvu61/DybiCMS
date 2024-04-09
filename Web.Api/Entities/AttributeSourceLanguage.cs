namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AttributeSourceLanguage
    {
        [Required]
        public Guid AttributeSourceId { get; set; }

        [ForeignKey("AttributeSourceId")]
        public AttributeSource Source { get; set; }

        [Required]
        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
