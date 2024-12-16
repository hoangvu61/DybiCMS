
namespace Web.Model
{
    using SeedWork;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ArticleModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Type { get; set; }

        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate {
            get {
                if (CreateDate.Year == DateTime.Now.Year) return CreateDate;
                else {
                    int month = CreateDate.Month < DateTime.Now.Month ? CreateDate.Month : DateTime.Now.Month;
                    int day = CreateDate.Day < DateTime.Now.Day ? CreateDate.Day : DateTime.Now.Day;
                    return new DateTime(DateTime.Now.Year, month, day);
                }
            } }
        public int Views { get; set; }
        public bool IsPublished { get; set; }
        public string HTML { get; set; }

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

        public ArticleModel()
        {
            Title = Brief = Content = string.Empty;
        }

        public string GetAttribute(string key)
        {
            return Attributes.Where(e => e.Id == key).Select(e => e.ValueName).FirstOrDefault();
        }
    }
}  
