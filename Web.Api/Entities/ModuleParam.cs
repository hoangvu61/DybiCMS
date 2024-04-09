namespace Web.Api.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class ModuleParam
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string ParamName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string ModuleName { get; set; }

        [ForeignKey("ModuleName")]
        public Module Module { get; set; }

        [Required]
        [MaxLength(250)]
        public string Describe { get; set; }

        [MaxLength(250)]
        [AllowNull]
        public string? DefaultValue { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "VARCHAR")]
        public string Type { get; set; }
    }
}
