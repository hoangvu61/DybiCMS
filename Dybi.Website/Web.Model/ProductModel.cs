
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Model.SeedWork;

namespace Web.Model
{
    public partial class ProductModel
    {
		public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryBrief { get; set; }
        public string ComponentList { get; set; }
        public string ComponentDetail { get; set; }

        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Views { get; set; }
        public bool IsPublished { get; set; }

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
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal PriceAfterDiscount { get
            {
                var price = Price;
                if (DiscountType > 0)
                {
                    if (DiscountType == 1) price = Price - (Discount * Price) / 100;
                    else if (DiscountType == 2) price = Price - Discount;
                    else if (DiscountType == 3) price = Discount;
                }
                return price;
            }
        }

        public int DiscountType { get; set; }

        public int SaleMin { get; set; }

        public List<ItemAttributeModel> Attributes { get; set; }

        public ProductModel()
        {
            Title = Brief = Content = string.Empty;
        }

        public string GetAttribute(string key)
        {
            return Attributes.Where(e => e.Id == key).Select(e => e.ValueName).FirstOrDefault();
        }
        public string GetAttributeValueId(string key)
        {
            return Attributes.Where(e => e.Id == key).Select(e => e.Value).FirstOrDefault();
        }
    }
}  
