using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class MediaListSearch : PagingParameters
    {
        public MediaListSearch() { 
            PageSize = 10;
            Type = "LIN";
        }
        public Guid? CategoryId { get; set; }

        public string Type { get; set; }

        public string? Title { get; set; }
    }
}
