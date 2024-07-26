using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public interface IWarehouseRepository
    {
        Task<List<Config>> GetConfigDefaults();
        Task<List<WarehouseConfig>> GetConfigs(Guid companyId);
        Task<string?> GetConfig(Guid companyId, string key);
        Task<WarehouseConfig> SetConfig(Guid companyId, string key, string? value);
    }
}
