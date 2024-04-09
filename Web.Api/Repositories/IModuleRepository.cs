using Web.Api.Entities;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public interface IModuleRepository
    {
        Task<PagedList<Module>> GetModuleList(PagingParameters paging);
        Task<Module> GetModuleByName(string moduleName);
        Task<Module> UpdateModule(Module module);
        Task<Module> CreateModule(Module module);
        Task<Module> DeleteModule(Module module);

        Task<List<ModuleParam>> GetParamsByModuleName(string moduleName);
        Task<PagedList<ModuleParam>> GetParamsByModuleName(string moduleName, PagingParameters paging);
        Task<ModuleParam> GetParamByName(string moduleName, string paramName);
        Task<ModuleParam> CreateParam(ModuleParam param);
        Task<ModuleParam> UpdateParam(ModuleParam param);
        Task<ModuleParam> DeleteParam(ModuleParam param);
    }
}
