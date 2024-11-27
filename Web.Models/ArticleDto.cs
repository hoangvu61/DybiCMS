using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ArticleDto
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public Dictionary<string, string> CategoryNames { get; set; }
        public Dictionary<string, string> Titles { get; set; }

        public int Order { get; set; }

        public bool IsPublished { get; set; }

        public DateTime DisplayDate { get; set; }

        public FileData? Image { get; set; }

        public int View { get; set; }

        public Guid CreateBy { get; set; }
        public string Link { get; set; }
    }
}
