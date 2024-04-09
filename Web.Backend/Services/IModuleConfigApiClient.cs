using Web.Models;
using Web.Models.SeedWork;

namespace Web.Backend.Services
{
    public interface IModuleConfigApiClient
    {
        Task<PagedList<ModuleConfigDto>> GetModuleList(ModuleConfigListSearch paging);
        Task<ModuleConfigDto> GetModule(string id);
        Task<bool> CreateModule(ModuleConfigCreateRequest request);
        Task<bool> UpdateApplyModule(Guid id);
        Task<bool> UpdateOrderModule(Guid id, int order);
        Task<bool> DeleteModule(Guid id);

        Task<bool> UpdateTitle(Guid id, TitleLanguageDto request);

        Task<List<ModuleParamDto>> GetParamsByModule(Guid id);
        Task<bool> UpdateParam(Guid id, ModuleConfigParamDto request);

        Task<ModuleConfigSkinDto> GetSkin(Guid id);
        Task<bool> UpdateSkin(Guid id, ModuleConfigSkinDto request);
    }
}
