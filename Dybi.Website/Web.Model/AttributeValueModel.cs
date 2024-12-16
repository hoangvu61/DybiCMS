
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class AttributeValueModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid? SourceId { get; set; }
        public string SourceName { get; set; }
    }
}  
