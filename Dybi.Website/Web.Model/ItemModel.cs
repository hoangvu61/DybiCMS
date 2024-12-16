
namespace Web.Model
{
    using SeedWork;
    using System;
    using System.Collections.Generic;

    public partial class ItemModel
    {
		public Guid Id { get; set; }
        public FileData Image { get; set; }
        public string Title { get; set; }
		public string Brief { get; set; }
		public string Tags { get; set; }
        public int Views { get; set; }
    }
}  
