using Web.Models;
using Web.Models.SeedWork;

namespace Dybi.App.Services
{
    public interface IWarehouseApiClient
    {
        Task<List<ConfigDefaultDto>> GetConfigDefaults();
        Task<List<ConfigDto>> GetConfigs();
        Task<string> GetConfig(string key);
        Task<bool> SetConfig(string key, string value);
    }
}
