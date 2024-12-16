
namespace Web.Model
{
    using SeedWork;
    using System;
    using System.Collections.Generic;

    public partial class CategoryModel
    {
		public Guid Id { get; set; }
        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Views { get; set; }
        public bool IsPublished { get; set; }

        public Guid? ParentId { get; set; }
        public string ParentTitle { get; set; }
        public string Type { get; set; }

        public string ComponentList { get; set; }
        public string ComponentDetail { get; set; }

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

        public bool IsSEO { get; set; }

        public FileData Image { get; set; }
        
        public CategoryModel()
        {
            Title = Brief = Content = string.Empty;
        }
    }
}  
