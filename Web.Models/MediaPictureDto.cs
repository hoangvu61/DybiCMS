using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class MediaPictureDto
    {
        public Guid CategoryId { get; set; }

        public FileData Image { get; set; }
    }
}
