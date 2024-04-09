namespace Web.Api.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TemplateLanguage
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageKey { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        [Required]
        public string TemplateName { get; set; }

        [ForeignKey("TemplateName")]
        public Template Template { get; set; }
    }
}
