namespace Web.Models
{
    public partial class AttributeOrderContactDto
    {
        public string AttributeId { get; set; }

        public int Order { get; set; }
        public Dictionary<string, string> AttributeTitles { get; set; }
    }
}
