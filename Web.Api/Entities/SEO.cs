namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SEO
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public Guid? ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item? Item { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }

        public Guid? NoItemId { get; set; }

        [Required]
        public string Url { get; set; }


        [Required]
        public string SeoUrl { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string MetaKeyWord { get; set; }

        [Required]
        [MaxLength(500)]
        public string MetaDescription { get; set; }
    }
}
