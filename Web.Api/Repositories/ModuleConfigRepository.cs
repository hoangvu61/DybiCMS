
using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public class ModuleConfigRepository : IModuleConfigRepository
    {
        private readonly WebDbContext _context;
        public ModuleConfigRepository(WebDbContext context)
        {
            _context = context;
        }


        public async Task<PagedList<ModuleConfig>> GetModules(Guid companyId, ModuleConfigListSearch moduleConfigSearch)
        {
            var query = _context.ModuleConfigs.Include(e => e.ModuleConfigDetails).Where(e => e.CompanyId == companyId);

            if (!string.IsNullOrEmpty(moduleConfigSearch.Name)) 
                query = query.Where(e => e.ModuleConfigDetails.Any(d => d.Title.Contains(moduleConfigSearch.Name)));

            if (!string.IsNullOrEmpty(moduleConfigSearch.ComponentName) && moduleConfigSearch.ComponentName != "*")
                if (moduleConfigSearch.ComponentName == "_") query = query.Where(e => e.OnTemplate);
                else query = query.Where(e => e.ComponentName == moduleConfigSearch.ComponentName);

            if (!string.IsNullOrEmpty(moduleConfigSearch.Position))
                query = query.Where(e => e.Position == moduleConfigSearch.Position);

            var count = await query.CountAsync();

            var data = await query.OrderBy(e => e.ComponentName).ThenBy(e => e.Position).ThenBy(e => e.Order)
                .Skip((moduleConfigSearch.PageNumber - 1) * moduleConfigSearch.PageSize)
                .Take(moduleConfigSearch.PageSize)
                .ToListAsync();
            return new PagedList<ModuleConfig>(data, count, moduleConfigSearch.PageNumber, moduleConfigSearch.PageSize);

        }

        public async Task<ModuleConfig> GetModule(Guid moduleId)
        {
            var module = await _context.ModuleConfigs.Include(e => e.ModuleConfigDetails).FirstOrDefaultAsync(e => e.Id == moduleId);
            return module;
        }

        public async Task<ModuleConfig> CreateModule(ModuleConfig moduleConfig)
        {
            _context.ModuleConfigs.Add(moduleConfig);
            _context.ModuleSkins.Add(new ModuleSkin
            {
                Id = moduleConfig.Id,
                HeaderFontSize = 0,
                BodyFontSize = 0,
                Width = 0,
                Height = 0,
            });

            var languages = await _context.CompanyLanguages.Where(e => e.CompanyId == moduleConfig.CompanyId).ToListAsync();
            foreach(var language in languages)
            {
                _context.ModuleConfigDetails.Add(new ModuleConfigDetail
                {
                    ModuleId = moduleConfig.Id,
                    LanguageCode = language.LanguageCode,
                    Title = "Tiêu đề " + language.LanguageCode
                });
            }

            var webconfig = await _context.WebConfigs.FindAsync(moduleConfig.CompanyId);
            var skin = await _context.TemplateSkins.FirstOrDefaultAsync(e => e.TemplateName == webconfig.TemplateName && e.SkinName == moduleConfig.SkinName);
            var paramList = await _context.ModuleParams.Where(e => e.ModuleName == skin.ModuleName).ToListAsync();
            foreach (var param in paramList)
            {
                var paramConfig = new ModuleConfigParam
                {
                    ModuleId = moduleConfig.Id,
                    ParamName = param.ParamName,
                    Value = param.DefaultValue ?? string.Empty
                };   
                _context.ModuleConfigParams.Add(paramConfig);
            }

            await _context.SaveChangesAsync();
            return moduleConfig;
        }
        public async Task<ModuleConfig> ApplyChange(Guid companyId, Guid moduleConfigId)
        {
            var moduleConfig = await _context.ModuleConfigs.FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Id == moduleConfigId);
            if (moduleConfig != null)
            {
                moduleConfig.Apply = !moduleConfig.Apply;
                _context.ModuleConfigs.Update(moduleConfig);
                await _context.SaveChangesAsync();
            }

            return moduleConfig;
        }        
        public async Task<ModuleConfig> OrderUpdate(Guid companyId, Guid moduleConfigId, int order)
        {
            var moduleConfig = await _context.ModuleConfigs.FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Id == moduleConfigId);
            if (moduleConfig != null)
            {
                moduleConfig.Order = order;
                _context.ModuleConfigs.Update(moduleConfig);
                await _context.SaveChangesAsync();
            }

            return moduleConfig;
        }
        public async Task<bool> DeleteModule(Guid companyId, Guid moduleConfigId)
        {
            var moduleConfig = await _context.ModuleConfigs.FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Id == moduleConfigId);
            if (moduleConfig == null) return false;

            _context.ModuleConfigs.Remove(moduleConfig);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task TitleChange(Guid companyId, Guid moduleConfigId, string languageCode, string title)
        {
            var detail = await _context.ModuleConfigDetails
                    .FirstOrDefaultAsync(e => e.ModuleConfig.CompanyId == companyId 
                                            && e.ModuleId == moduleConfigId
                                            && e.LanguageCode == languageCode);
            if (detail != null)
            {
                detail.Title = title;
                _context.ModuleConfigDetails.Update(detail);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.ModuleConfigDetails.Add(new ModuleConfigDetail
                {
                    ModuleId = moduleConfigId,
                    LanguageCode = languageCode,
                    Title = title
                });
                await _context.SaveChangesAsync();
            } 
        }

        public async Task<List<ModuleConfigParam>> GetParamsByModule(Guid moduleConfigId)
        {
            var query = _context.ModuleConfigParams.Where(e => e.ModuleId == moduleConfigId);
            var data = await query.ToListAsync();
            return data;

        }

        public async Task UpdateParam(Guid companyId, Guid moduleConfigId, string paramName, string value)
        {
            var param = await _context.ModuleConfigParams
                    .FirstOrDefaultAsync(e => e.ModuleConfig.CompanyId == companyId
                                            && e.ModuleId == moduleConfigId
                                            && e.ParamName == paramName);
            if (param != null)
            {
                param.Value = value;
                _context.ModuleConfigParams.Update(param);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.ModuleConfigParams.Add(new ModuleConfigParam
                {
                    ModuleId = moduleConfigId,
                    ParamName = paramName,
                    Value = value
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ModuleSkin> GetSkin(Guid moduleId)
        {
            var module = await _context.ModuleSkins.FirstOrDefaultAsync(e => e.Id == moduleId);
            return module;
        }

        public async Task UpdateSkin(ModuleSkin skin)
        {
            _context.ModuleSkins.Update(skin);
            await _context.SaveChangesAsync();
        }
    }
}
