namespace Web.Models
{
    public partial class ModuleConfigDto
    {
        public Guid Id { get; set; }
        public string SkinName { get; set; }
        public string SkinDescribe { get; set; }
        public bool OnTemplate { get; set; }
        public string? ComponentName { get; set; }
        public string ComponentDescribe { get; set; }
        public string Position { get; set; }
        public string PositionDescribe { get; set; }
        public int Order { get; set; }
        public bool Apply { get; set; }
        public Dictionary<string,string> Titles { get; set; }
    }
}
