using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class EventDto
    {
        public Guid Id { get; set; }
        public Dictionary<string, string> Titles { get; set; }

        public int Order { get; set; }

        public bool IsPublished { get; set; }

        public DateTime StartDate { get; set; }

        public FileData? Image { get; set; }

        public int View { get; set; }
        public string Place { get; set; }
    }
}
