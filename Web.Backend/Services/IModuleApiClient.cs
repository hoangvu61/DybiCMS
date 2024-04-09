using Web.Models;
using Web.Models.SeedWork;

namespace Web.Backend.Services
{
    public interface IModuleApiClient
    {
        Task<PagedList<ModuleDto>> GetModuleList(PagingParameters paging);

        Task<bool> DeleteModule(string id);

        Task<bool> CreateModule(ModuleDto request);
        Task<bool> UpdateModule(ModuleDto request);

        Task<PagedList<ModuleParamDto>> GetModuleParamList(string moduleName, PagingParameters paging);
        Task<ModuleParamDto> GetModuleParam(string moduleName, string paramName);
        Task<bool> CreateModuleParam(ModuleParamDto request);
        Task<bool> UpdateModuleParam(ModuleParamDto request);
        Task<bool> DeleteModuleParam(string moduleName, string paramName);
    }
}
