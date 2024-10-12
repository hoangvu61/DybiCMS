using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ReviewListSearch : PagingParameters
    {
        public string? Key { get; set; }
        public Guid? ReviewFor { get; set; }

        public bool? Approved { get; set; }
    }
}
