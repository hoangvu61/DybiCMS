using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ProductListSearch : PagingParameters
    {
        [AllowNull]
        public string? Key { get; set; }

        [AllowNull]
        public Guid? CategoryId { get; set; }
    }
}
