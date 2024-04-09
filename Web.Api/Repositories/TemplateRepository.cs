using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models.SeedWork;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Api.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly WebDbContext _context;
        public TemplateRepository(WebDbContext context)
        {
            _context = context;
        }

        #region Template
        public async Task<PagedList<Template>> GetTemplates(PagingParameters paging)
        {
            var count = await _context.Templates.CountAsync();
            List<Template> data; 
            if (paging.PageSize > 0)
            {
                data = await _context.Templates.OrderBy(x => x.TemplateName)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();
            }
            else
            {
                data = await _context.Templates.ToListAsync();
            }

            return new PagedList<Template>(data, count, paging.PageNumber, paging.PageSize);
        }
        public async Task<Template> GetTemplateByName(string templateName)
        {
            return await _context.Templates.FindAsync(templateName);
        }
        public async Task<Template> CreateTemplate(Template template)
        {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();
            return template;
        }
        public async Task<Template> UpdateTemplate(Template template)
        {
            _context.Templates.Update(template);
            await _context.SaveChangesAsync();
            return template;
        }
        public async Task<Template> DeleteTemplate(Template template)
        {
            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
            return template;
        }
        #endregion

        #region Skin
        public async Task<List<TemplateSkin>> GetSkinsByCompany(Guid companyId)
        {
            var webconfig = await _context.WebConfigs.FirstOrDefaultAsync(e => e.Id == companyId);
            if (webconfig == null) return new List<TemplateSkin>();

            var query = _context.TemplateSkins.Where(e => e.TemplateName == webconfig.TemplateName);

            var data = await query.OrderBy(x => x.SkinName).ToListAsync();
            return data;
        }
        public async Task<PagedList<TemplateSkin>> GetSkinsByTemplateName(string templateName, PagingParameters paging)
        {
            var query = _context.TemplateSkins.Where(e => e.TemplateName == templateName);

            var count = await query.CountAsync();

            var data = await query.OrderBy(x => x.SkinName)
            .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
            .ToListAsync();
            return new PagedList<TemplateSkin>(data, count, paging.PageNumber, paging.PageSize);
        }
        public async Task<TemplateSkin> GetSkinByName(string templateName, string skinName)
        {
            return await _context.TemplateSkins.FirstOrDefaultAsync(p => p.TemplateName == templateName && p.SkinName == skinName);
        }

        public async Task<TemplateSkin> CreateSkin(TemplateSkin skin)
        {
            _context.TemplateSkins.Add(skin);
            await _context.SaveChangesAsync();
            return skin;
        }
        public async Task<TemplateSkin> UpdateSkin(TemplateSkin skin)
        {
            _context.TemplateSkins.Update(skin);
            await _context.SaveChangesAsync();
            return skin;
        }
        public async Task<TemplateSkin> DeleteSkin(TemplateSkin skin)
        {
            _context.TemplateSkins.Remove(skin);
            await _context.SaveChangesAsync();
            return skin;
        }
        #endregion

        #region Component
        public async Task<List<TemplateComponent>> GetComponentsByCompany(Guid companyId)
        {
            var webconfig = await _context.WebConfigs.FirstOrDefaultAsync(e => e.Id == companyId);
            if (webconfig == null) return new List<TemplateComponent>();

            var query = _context.TemplateComponents.Where(e => e.TemplateName == webconfig.TemplateName);

            var data = await query.OrderByDescending(x => x.ComponentName).ToListAsync();
            return data;
        }
        public async Task<PagedList<TemplateComponent>> GetComponentsByTemplateName(string templateName, PagingParameters paging)
        {
            var query = _context.TemplateComponents.Where(e => e.TemplateName == templateName);

            var count = await query.CountAsync();

            List<TemplateComponent> data;
            if (paging.PageSize > 0)
            {
                data = await query.OrderBy(x => x.ComponentName)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();
            }
            else
            {
                data = await query.OrderByDescending(x => x.ComponentName).ToListAsync();
            }
            return new PagedList<TemplateComponent>(data, count, paging.PageNumber, paging.PageSize);
        }
        public async Task<TemplateComponent> GetComponentByName(string templateName, string componentName)
        {
            return await _context.TemplateComponents.FirstOrDefaultAsync(p => p.TemplateName == templateName && p.ComponentName == componentName);
        }
        public async Task<TemplateComponent> CreateComponent(TemplateComponent component)
        {
            _context.TemplateComponents.Add(component);
            await _context.SaveChangesAsync();
            return component;
        }
        public async Task<TemplateComponent> UpdateComponent(TemplateComponent component)
        {
            _context.TemplateComponents.Update(component);
            await _context.SaveChangesAsync();
            return component;
        }
        public async Task<TemplateComponent> DeleteComponent(TemplateComponent component)
        {
            _context.TemplateComponents.Remove(component);
            await _context.SaveChangesAsync();
            return component;
        }
        #endregion

        #region Position
        public async Task<List<TemplatePosition>> GetPositionsByCompany(Guid companyId)
        {
            var webconfig = await _context.WebConfigs.FirstOrDefaultAsync(e => e.Id == companyId);
            if (webconfig == null) return new List<TemplatePosition>();

            var query = _context.TemplatePositions.Where(e => e.TemplateName == webconfig.TemplateName);

            var data = await query.OrderBy(x => x.ComponentName).ThenBy(e => e.PositionName)
            .ToListAsync();
            return data;
        }
        public async Task<PagedList<TemplatePosition>> GetPositionsByTemplateName(string templateName, PagingParameters paging)
        {
            var query = _context.TemplatePositions.Where(e => e.TemplateName == templateName);

            var count = await query.CountAsync();

            var data = await query.OrderBy(x => x.ComponentName).ThenBy(e => e.PositionName)
            .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
            .ToListAsync();
            return new PagedList<TemplatePosition>(data, count, paging.PageNumber, paging.PageSize);
        }
        public async Task<TemplatePosition> GetPositionByName(string templateName, string componentName, string positionName)
        {
            return await _context.TemplatePositions.FirstOrDefaultAsync(p => p.TemplateName == templateName && p.ComponentName == componentName && p.PositionName == positionName);
        }
        public async Task<TemplatePosition> CreatePosition(TemplatePosition position)
        {
            _context.TemplatePositions.Add(position);
            await _context.SaveChangesAsync();
            return position;
        }
        public async Task<TemplatePosition> UpdatePosition(TemplatePosition position)
        {
            _context.TemplatePositions.Update(position);
            await _context.SaveChangesAsync();
            return position;
        }
        public async Task<TemplatePosition> DeletePosition(TemplatePosition position)
        {
            _context.TemplatePositions.Remove(position);
            await _context.SaveChangesAsync();
            return position;
        }
        #endregion


        #region LanguageKey
        public async Task<PagedList<TemplateLanguage>> GetLanguageKeys(string templateName, PagingParameters paging)
        {
            var query = _context.TemplateLanguageKeys.Where(e => e.TemplateName == templateName);

            var count = await query.CountAsync();

            List<TemplateLanguage> data;
            if (paging.PageSize > 0)
            {
                data = await query.OrderBy(x => x.LanguageKey)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                .ToListAsync();
            }
            else
            {
                data = await query.OrderByDescending(x => x.LanguageKey).ToListAsync();
            }
            return new PagedList<TemplateLanguage>(data, count, paging.PageNumber, paging.PageSize);
        }
        public async Task<TemplateLanguage> GetLanguageKey(string templateName, string key)
        {
            return await _context.TemplateLanguageKeys.FirstOrDefaultAsync(p => p.TemplateName == templateName && p.LanguageKey == key);
        }

        public async Task<TemplateLanguage> CreateLanguageKey(TemplateLanguage key)
        {
            _context.TemplateLanguageKeys.Add(key);
            await _context.SaveChangesAsync();
            return key;
        }
        public async Task<TemplateLanguage> UpdateLanguageKey(TemplateLanguage key)
        {
            _context.TemplateLanguageKeys.Update(key);
            await _context.SaveChangesAsync();
            return key;
        }
        public async Task<TemplateLanguage> DeleteLanguageKey(TemplateLanguage key)
        {
            var configs = await _context.CompanyLanguageConfigs.Where(e => e.Company.WebConfig.TemplateName == key.TemplateName && e.LanguageKey == key.LanguageKey).ToArrayAsync();
            _context.CompanyLanguageConfigs.RemoveRange(configs);
            _context.TemplateLanguageKeys.Remove(key);
            await _context.SaveChangesAsync();
            return key;
        }
        #endregion
    }
}
