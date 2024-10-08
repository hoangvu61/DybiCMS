using Web.Models;

namespace Web.App.Services
{
    public class GlobalVariables
    {
        public List<ConfigDto> Configs { get; set; }

        public string GetConfig(string key)
        {
            return Configs.Where(x => x.Key == key).Select(e => e.Value).FirstOrDefault();
        }
    }
}
