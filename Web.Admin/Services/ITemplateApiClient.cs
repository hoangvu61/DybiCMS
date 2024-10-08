using Web.Models;
using Web.Models.SeedWork;

namespace Web.Admin.Services
{
    public interface ITemplateApiClient
    {
        Task<PagedList<TemplateDto>> GetTemplateList(PagingParameters paging);
        Task<TemplateDto> GetTemplateByName(string templateName);
        Task<bool> CreateTemplate(TemplateDto request);
        Task<bool> UpdateTemplate(TemplateDto request);
        Task<bool> DeleteTemplate(string id);

        Task<List<TemplateSkinDto>> GetSkins();
        Task<PagedList<TemplateSkinDto>> GetTemplateSkinList(string templateName, PagingParameters paging);
        Task<TemplateSkinDto> GetTemplateSkin(string templateName, string skinName);
        Task<bool> CreateTemplateSkin(TemplateSkinDto request);
        Task<bool> UpdateTemplateSkin(TemplateSkinDto request);
        Task<bool> DeleteTemplateSkin(string templateName, string skinName);

        Task<List<TemplateComponentDto>> GetComponents();
        Task<PagedList<TemplateComponentDto>> GetTemplateComponentList(string templateName, PagingParameters paging);
        Task<TemplateComponentDto> GetTemplateComponent(string templateName, string componentName);
        Task<bool> CreateTemplateComponent(TemplateComponentDto request);
        Task<bool> UpdateTemplateComponent(TemplateComponentDto request);
        Task<bool> DeleteTemplateComponent(string templateName, string componentName);

        Task<List<TemplatePositionDto>> GetPositions();
        Task<PagedList<TemplatePositionDto>> GetTemplatePositionList(string templateName, PagingParameters paging);
        Task<TemplatePositionDto> GetTemplatePosition(string templateName, string positionName, string componentName);
        Task<bool> CreateTemplatePosition(TemplatePositionDto request);
        Task<bool> UpdateTemplatePosition(TemplatePositionDto request);
        Task<bool> DeleteTemplatePosition(string templateName, string positionName, string componentName);

        Task<PagedList<string>> GetTemplateLanguageKeys(string templateName, PagingParameters paging);
        Task<bool> CreateTemplateLanguageKey(TemplateLanguageDto request);
        Task<bool> DeleteTemplateLanguageKey(string templateName, string key);
    }
}
