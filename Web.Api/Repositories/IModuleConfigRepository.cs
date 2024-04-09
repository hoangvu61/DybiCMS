using Web.Api.Entities;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public interface IModuleConfigRepository
    {
        Task<PagedList<ModuleConfig>> GetModules(Guid companyId, ModuleConfigListSearch moduleConfigSearch);
        Task<ModuleConfig> GetModule(Guid moduleId);
        Task<ModuleConfig> CreateModule(ModuleConfig moduleConfig);
        Task<ModuleConfig> ApplyChange(Guid companyId, Guid moduleConfigId);
        Task<ModuleConfig> OrderUpdate(Guid companyId, Guid moduleConfigId, int order);
        Task<bool> DeleteModule(Guid companyId, Guid moduleConfigId);

        Task TitleChange(Guid companyId, Guid moduleConfigId, string languageCode, string title);

        Task<List<ModuleConfigParam>> GetParamsByModule(Guid moduleConfigId);

        Task UpdateParam(Guid companyId, Guid moduleConfigId, string param, string value);

        Task<ModuleSkin> GetSkin(Guid moduleId);
        Task UpdateSkin(ModuleSkin skin);
    }
}
