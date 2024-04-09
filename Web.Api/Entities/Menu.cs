namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class Menu
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Id")]
        public Item Item { get; set; }

        [Required]
        [MaxLength(10)]
        [Column(TypeName = "VARCHAR")]
        public string Type { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string Component { get; set; }

        public int Order { get; set; }
    }
}
