using Web.Api.Entities;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public interface ITemplateRepository
    {
        Task<PagedList<Template>> GetTemplates(PagingParameters paging);
        Task<Template> GetTemplateByName(string templateName);
        Task<Template> CreateTemplate(Template template);
        Task<Template> UpdateTemplate(Template template);
        Task<Template> DeleteTemplate(Template template);

        Task<List<TemplateComponent>> GetComponentsByCompany(Guid id);
        Task<PagedList<TemplateComponent>> GetComponentsByTemplateName(string templateName, PagingParameters paging);
        Task<TemplateComponent> GetComponentByName(string templateName, string componentName);
        Task<TemplateComponent> CreateComponent(TemplateComponent component);
        Task<TemplateComponent> UpdateComponent(TemplateComponent component);
        Task<TemplateComponent> DeleteComponent(TemplateComponent component);

        Task<List<TemplateSkin>> GetSkinsByCompany(Guid companyId);
        Task<PagedList<TemplateSkin>> GetSkinsByTemplateName(string templateName, PagingParameters paging);
        Task<TemplateSkin> GetSkinByName(string templateName, string skinName);
        Task<TemplateSkin> CreateSkin(TemplateSkin skin);
        Task<TemplateSkin> UpdateSkin(TemplateSkin skin);
        Task<TemplateSkin> DeleteSkin(TemplateSkin skin);

        Task<List<TemplatePosition>> GetPositionsByCompany(Guid companyId);
        Task<PagedList<TemplatePosition>> GetPositionsByTemplateName(string templateName, PagingParameters paging);
        Task<TemplatePosition> GetPositionByName(string templateName, string positionName, string componentName);
        Task<TemplatePosition> CreatePosition(TemplatePosition position);
        Task<TemplatePosition> UpdatePosition(TemplatePosition position);
        Task<TemplatePosition> DeletePosition(TemplatePosition position);

        Task<PagedList<TemplateLanguage>> GetLanguageKeys(string templateName, PagingParameters paging);
        Task<TemplateLanguage> GetLanguageKey(string templateName, string key);
        Task<TemplateLanguage> CreateLanguageKey(TemplateLanguage key);
        Task<TemplateLanguage> UpdateLanguageKey(TemplateLanguage key);
        Task<TemplateLanguage> DeleteLanguageKey(TemplateLanguage key);
    }
}
