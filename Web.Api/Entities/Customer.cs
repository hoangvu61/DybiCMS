namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class Customer
    {

        [Key]
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [Required]
        [MaxLength(200)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(30)]
        public string CustomerPhone { get; set; }

        [AllowNull]
        [MaxLength(300)]
        public string? CustomerAddress { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<DebtCustomer> Debts { get; set; }
    }
}
