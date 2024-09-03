using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class CustomerSearch : PagingParameters
    {
        [AllowNull]
        public string? Key { get; set; }
    }
}
