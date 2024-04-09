using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ArticleListSearch : PagingParameters
    {
        public string? Title { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Tag { get; set; }
    }
}
