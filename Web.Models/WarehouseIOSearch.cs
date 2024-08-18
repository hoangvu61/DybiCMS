using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class WarehouseIOSearch : PagingParameters
    {
        [AllowNull]
        public DateTime? FromDate { get; set; }

        [AllowNull]
        public DateTime? ToDate { get; set; }

        [AllowNull]
        public string? Code { get; set; }

        [AllowNull]
        public Guid? SupplyerId { get; set; }

        [AllowNull]
        public Guid? FactoryId { get; set; }

        [AllowNull]
        public Guid? WarehouseId { get; set; }

        [AllowNull]
        public Guid? FromOrToWarehouseId { get; set; }
    }
}
