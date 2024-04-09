using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ModuleConfigListSearch : PagingParameters
    {
        public string? Name { get; set; }
        public string? ComponentName { get; set; }
        public string? Position { get; set; }
    }
}
