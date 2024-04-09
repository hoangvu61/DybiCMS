namespace Web.Api.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Module
    {
        [Key]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string ModuleName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Describe { get; set; }
    }
}
