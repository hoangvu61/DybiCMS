
namespace Web.Model
{
    using SeedWork;
    using System;
    using System.Collections.Generic;

    public partial class ReviewModel
    {
        public Guid Id { get; set; }
        public Guid ReviewForId { get; set; } 
        public string ReviewForTitle { get; set; }
        public string ReviewForType { get; set; }
        public int Vote { get; set; }
        public string Comment { get; set; }
        public List<FileData> Images { get; set; }
        public System.DateTime Date { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsBuyer { get; set; }
        public List<ReviewModel> Replies { get; set; }
    }
}
