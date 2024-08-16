using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class WarehouseInventorySearch : PagingParameters
    {
        [AllowNull]
        public Guid? WarehouseId { get; set; }

        [AllowNull]
        public string? Key { get; set; }

        [AllowNull]
        public Guid? CategoryId { get; set; }

        [AllowNull]
        public bool IsAlertEmpty { get; set; } = false;
    }
}
