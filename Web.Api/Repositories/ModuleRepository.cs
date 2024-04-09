
using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly WebDbContext _context;
        public ModuleRepository(WebDbContext context)
        {
            _context = context;
        }

        #region module
        public async Task<PagedList<Module>> GetModuleList(PagingParameters paging)
        {
            var count = await _context.Modules.CountAsync();

            List<Module> data;
            if (paging.PageSize > 0)
            {
                data = await _context.Modules.OrderByDescending(x => x.ModuleName)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();
            }
            else
            {
                data = await _context.Modules.OrderByDescending(x => x.ModuleName).ToListAsync();
            }

            return new PagedList<Module>(data, count, paging.PageNumber, paging.PageSize);
        }

        public async Task<Module> GetModuleByName(string moduleName)
        {
            return await _context.Modules.FindAsync(moduleName);
        }
        public async Task<Module> CreateModule(Module module)
        {
            _context.Modules.Add(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task<Module> UpdateModule(Module module)
        {
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();
            return module;
        }

        public async Task<Module> DeleteModule(Module module)
        {
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return module;
        }
        #endregion

        #region Param
        public async Task<List<ModuleParam>> GetParamsByModuleName(string moduleName)
        {
            var query = _context.ModuleParams.Where(e => e.ModuleName == moduleName);
            var data = await query.ToListAsync();
            return data;
        }
        public async Task<PagedList<ModuleParam>> GetParamsByModuleName(string moduleName, PagingParameters paging)
        {
            var query = _context.ModuleParams.Where(e => e.ModuleName == moduleName);
            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.ParamName)
            .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
            .ToListAsync();
            return new PagedList<ModuleParam>(data, count, paging.PageNumber, paging.PageSize);
        }
        
        public async Task<ModuleParam> GetParamByName(string moduleName, string paramName)
        {
            return await _context.ModuleParams.FirstOrDefaultAsync(p => p.ModuleName == moduleName && p.ParamName == paramName);
        }

        public async Task<ModuleParam> CreateParam(ModuleParam param)
        {
            _context.ModuleParams.Add(param);
            await _context.SaveChangesAsync();
            return param;
        }

        public async Task<ModuleParam> UpdateParam(ModuleParam param)
        {
            _context.ModuleParams.Update(param);
            await _context.SaveChangesAsync();
            return param;
        }

        public async Task<ModuleParam> DeleteParam(ModuleParam param)
        {
            _context.ModuleParams.Remove(param);
            await _context.SaveChangesAsync();
            return param;
        }
        #endregion
    }
}
