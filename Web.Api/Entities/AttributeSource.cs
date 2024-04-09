namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AttributeSource
    {

        [Key]
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public virtual ICollection<AttributeSourceLanguage> AttributeSourceLanguages { get; set; }
        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
    }
}
