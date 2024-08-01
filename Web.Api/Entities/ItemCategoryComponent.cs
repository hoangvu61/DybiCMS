namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class ItemCategoryComponent
    {

        [Key]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public ItemCategory Category { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        [Required]
        public string ComponentList { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        [Required]
        public string ComponentDetail { get; set; }
    }
}
