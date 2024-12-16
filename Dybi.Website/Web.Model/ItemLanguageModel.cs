
using System;

namespace Web.Model
{
    public partial class ItemLanguageModel
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string Content { get; set; }
        public string TitleUnSign { get; set; }
    }
}  
