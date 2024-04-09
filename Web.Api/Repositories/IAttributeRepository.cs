using Web.Api.Entities;
using Web.Models.SeedWork;
using Web.Models;

namespace Web.Api.Repositories
{
    public interface IAttributeRepository
    {
        Task<List<Entities.Attribute>> GetAttributes(Guid companyId);
        Task<List<AttributeLanguage>> GetAttributes(Guid companyId, string languageCode);
        Task<List<AttributeLanguage>> GetAttributes(Guid companyId, string languageCode, List<Guid> categoryIds);
        Task<Entities.Attribute> GetAttribute(Guid companyId, string id);
        Task<Entities.Attribute> CreateAttribute(Entities.Attribute attribute);
        Task<Entities.Attribute> UpdateAttribute(Entities.Attribute attribute);
        Task<bool> DeleteAttribute(Entities.Attribute attribute);

        Task<List<AttributeSource>> GetAttributeSources(Guid companyId);
        Task<List<AttributeSourceLanguage>> GetAttributeSources(Guid companyId, string languageCode);
        Task<AttributeSource> GetAttributeSource(Guid companyId, Guid id);
        Task<AttributeSource> CreateAttributeSource(AttributeSource source);
        Task<AttributeSource> UpdateAttributeSource(AttributeSource source);
        Task<bool> DeleteAttributeSource(AttributeSource source);

        Task<List<AttributeValue>> GetAttributeValues(Guid companyId);
        Task<List<AttributeValueLanguage>> GetAttributeValues(Guid companyId, string language, List<Guid> sourceids);
        Task<AttributeValue> GetAttributeValue(Guid companyId, string id);
        Task<AttributeValue> CreateAttributeValue(AttributeValue value);
        Task<AttributeValue> UpdateAttributeValue(AttributeValue value);
        Task<bool> DeleteAttributeValue(AttributeValue value);

        Task<List<AttributeSourceLanguage>> GetAttributeSourceLanguages(Guid companyId, List<Guid> ids);
        Task<AttributeSourceLanguage> GetAttributeSourceLanguage(Guid companyId, Guid id, string languageCode);
        Task<AttributeSourceLanguage> CreateAttributeSourceLanguage(AttributeSourceLanguage value);
        Task<AttributeSourceLanguage> UpdateAttributeSourceLanguage(AttributeSourceLanguage value);
        Task<bool> DeleteAttributeSourceLanguage(AttributeSourceLanguage lang);

        Task<List<AttributeCategory>> GetAttributeCategories(Guid companyId);
        Task<AttributeCategory> GetAttributeCategory(Guid companyId, string attributeId, Guid categoryId);
        Task<AttributeCategory> CreateAttributeCategory(AttributeCategory attCat);
        Task<AttributeCategory> UpdateAttributeCategory(AttributeCategory attCat);
        Task<bool> DeleteAttributeCategory(AttributeCategory attCat);
    }
}
