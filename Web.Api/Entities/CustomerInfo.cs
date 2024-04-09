namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public partial class CustomerInfo
    {

        [Key]
        public Guid Id { get; set; }    

        [ForeignKey("Id")]
        public Customer Company { get; set; }

        [Required]
        [MaxLength(100)]
        public string InfoKey { get; set; }

        [AllowNull]
        public string InfoValue { get; set; }

        [AllowNull]
        [MaxLength(200)]
        public string InfoTitle { get; set; }
    }
}
