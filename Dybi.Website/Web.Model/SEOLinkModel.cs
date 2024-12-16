
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class SEOLinkModel
    {
        public string SeoUrl { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string MetaKeyWork { get; set; }
        public string MetaDescription { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? NoItemId { get; set; }
        public string LanguageCode { get; set; }
    }
}  
