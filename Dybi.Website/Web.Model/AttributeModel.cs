using System.Collections.Generic;

namespace Web.Model
{
    public partial class AttributeModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public System.Guid? SourceId { get; set; }
        public string SourceTitle { get; set; }
        public Dictionary<string, string> SourceValues { get; set; }
    }
}
