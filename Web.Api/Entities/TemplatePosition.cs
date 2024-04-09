namespace Web.Api.Entities
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TemplatePosition
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string PositionName { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        [Required]
        public string TemplateName { get; set; }

        [ForeignKey("TemplateName")]
        public Template Template { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string ComponentName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Describe { get; set; }

       
    }
}
