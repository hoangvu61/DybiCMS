namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ContactAttribute
    {
        [ForeignKey("Id")]
        public Guid ContactId { get; set; }

        [ForeignKey("Id")]
        public Contact Contact { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string AttributeId { get; set; }

        public string Value { get; set; }

    }
}
