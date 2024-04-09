namespace Web.Api.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ModuleConfigParam
    {
        [Required]
        public Guid ModuleId { get; set; }

        [ForeignKey("ModuleId")]
        public ModuleConfig ModuleConfig { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string ParamName { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
