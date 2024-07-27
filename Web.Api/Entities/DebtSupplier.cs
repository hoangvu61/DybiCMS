using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class DebtSupplier
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid SupplierId { get; set; }

        public decimal TotalDebt { get; set; }

        public decimal Price { get; set; }
        public DateTime? CreateDate { get; set; }

        [Description("1: Mình nợ; 2: Mình trả nợ")]
        [DefaultValue("0")]
        public int Type { get; set; }

        [ForeignKey("SupplierId")]
        public WarehouseSupplier Supplier { get; set; }
    }
}
