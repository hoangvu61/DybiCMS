using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public interface IWarehouseRepository
    {
        #region config
        Task<List<Config>> GetConfigDefaults();
        Task<List<WarehouseConfig>> GetConfigs(Guid companyId);
        Task<string?> GetConfig(Guid companyId, string key);
        Task<WarehouseConfig> SetConfig(Guid companyId, string key, string? value);
        #endregion

        #region warehouse
        Task<List<WarehouseInput>> GetInputs(Guid companyId);
        Task<ThirdParty> DeleteInput(ThirdParty thirdParty);

        #endregion
    }
}
