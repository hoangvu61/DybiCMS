
using System;

namespace Web.Model
{
    public partial class ItemAttributeModel
    {
		public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ValueName { get; set; }

        public int Order { get; set; }
    }
}  
