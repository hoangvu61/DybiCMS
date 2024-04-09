using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class EventListSearch : PagingParameters
    {
        public string? Title { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Place { get; set; }
    }
}
