
namespace Web.Model
{
    using SeedWork;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class MediaModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }

        public string ImageName { get; set; }
        public int Views { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsPublished { get; set; }

        public string Url { get; set; }
        public string Target { get; set; }
        public string Embed { get; set; }
        public string Type { get; set; }

        //private string title;
        //public string Title
        //{
        //    get
        //    {
        //        return this.title.Replace("[year]", DateTime.Now.Year.ToString())
        //                        .Replace("[date]", String.Format("{0:dd/MM/yy}", DateTime.Now));
        //    }
        //    set
        //    {
        //        this.title = value;
        //    }
        //}
        public string Title { get; set; }
        public string Brief { get; set; }
        public string Content { get; set; }

        public FileData Image { get; set; }

        public List<ItemAttributeModel> Attributes { get; set; }

        public MediaModel()
        {
            Title = Brief = Content = Embed = string.Empty;
        }

        public string GetAttribute(string key)
        {
            return Attributes.Where(e => e.Id == key).Select(e => e.ValueName).FirstOrDefault();
        }
    }
}  
