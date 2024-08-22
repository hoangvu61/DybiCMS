using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class DebtCustomerSearch : PagingParameters
    {
        [AllowNull]
        public DateTime? FromDate { get; set; }

        [AllowNull]
        public DateTime? ToDate { get; set; }

        [AllowNull]
        public int Type { get; set; }

        [AllowNull]
        public string? Key { get; set; }
    }
}
