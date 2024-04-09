using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class MediaDto
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public Dictionary<string, string> CategoryNames { get; set; }
        public Dictionary<string, string> Titles { get; set; }

        public int Order { get; set; }

        public bool IsPuslished { get; set; }

        public FileData? Image { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public string Embed { get; set; }
        public string Type { get; set; }
    }
}
