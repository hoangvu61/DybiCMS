
namespace Web.Model
{
    using SeedWork;
    using System;
    using System.Collections.Generic;

    public partial class ArticleBilingualModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Views { get; set; }
        public List<ItemLanguageModel> Details { get; set; }
        public FileData Image { get; set; }
    }
}  
