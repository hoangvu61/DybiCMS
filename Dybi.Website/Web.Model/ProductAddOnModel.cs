
using System;
using Web.Model.SeedWork;

namespace Web.Model
{
    public partial class ProductAddOnModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }

        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Views { get; set; }

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

        public string Code { get; set; }
        public decimal AddOnPrice { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}  
