namespace Web.Models.SeedWork
{
    public class CompanyType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }

        public CompanyType(string id, string name, string parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
        }
    }
}
