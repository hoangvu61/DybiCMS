using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ReviewListSearch : PagingParameters
    {
        public string? Name { get; set; }
        public Guid? ReviewFor { get; set; }
        public string? Phone { get; set; }

        public bool? Approved { get; set; }
    }
}
