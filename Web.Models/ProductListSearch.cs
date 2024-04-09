using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ProductListSearch : PagingParameters
    {
        public string? Title { get; set; }
        public string? Code { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
