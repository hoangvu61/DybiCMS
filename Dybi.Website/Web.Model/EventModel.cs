
namespace Web.Model
{
    using SeedWork;
    using System;

    public partial class EventModel
    {
        public Guid Id { get; set; }

        public string ImageName { get; set; }
        public DateTime StartDate { get; set; }
        public string Place { get; set; }
        public int Views { get; set; }
        public bool IsPublished { get; set; }

        public string Title { get; set; }
        public string Brief { get; set; }
        public string Content { get; set; }

        public FileData Image { get; set; }

        public EventModel()
        {
            Title = Brief = Content = string.Empty;
        }
    }
}  
