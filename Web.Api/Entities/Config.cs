using System.ComponentModel.DataAnnotations;

namespace Web.Api.Entities
{
    public partial class Config
    {
        [Key]
        [MaxLength(50)]
        public string Key { get; set; }

        [Required]
        [MaxLength(10)]
        public string Type { get; set; }

        [Required]
        public string DefaultValue { get; set; }

        [Required]
        public string Describe { get; set; }
        
    }
}
