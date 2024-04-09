using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public class Language
    {
        [Key]
        [MaxLength(5)]
        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Code { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
