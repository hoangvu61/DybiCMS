namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class Contact
    {

        [Key]
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [AllowNull]
        [MaxLength(200)]
        public string? ContactName { get; set; }

        [AllowNull]
        [MaxLength(30)]
        public string? CustomerPhone { get; set; }

        [AllowNull]
        [MaxLength(300)]
        public string? CustomerAddress { get; set; }

        [AllowNull]
        public string? Message { get; set; }

        public virtual ICollection<ContactAttribute> Attributes { get; set; }
    }
}
