using Web.Models;

namespace Web.Admin.Services
{
    public interface IAttributeApiClient
    {
        #region attribute
        Task<List<AttributeDto>> GetAttributeList();
        Task<List<TitleStringDto>> GetAttributeNameList(string language);
        Task<List<AttributeSetupDto>> GetAttributeSetupList(string language, Guid categoryid);
        Task<AttributeDetailDto> GetAttributeById(string id);
        Task<bool> CreateAttribute(AttributeDetailDto request);
        Task<bool> UpdateAttribute(AttributeDetailDto request);
        Task<bool> DeleteAttribute(string id);
        #endregion

        #region source
        Task<List<AttributeSourceDto>> GetSourceList();
        Task<List<TitleGuidDto>> GetSourceNameList(string language);
        Task<AttributeSourceDto> GetSourceById(Guid id);
        Task<bool> CreateSource(AttributeSourceDto request);
        Task<bool> UpdateSource(AttributeSourceDto request);
        Task<bool> DeleteSource(Guid id);
        #endregion

        #region value
        Task<List<AttributeValueDto>> GetValueList();
        Task<AttributeValueDto> GetValueById(string id);
        Task<bool> CreateValue(AttributeValueDto request);
        Task<bool> UpdateValue(AttributeValueDto request);
        Task<bool> DeleteValue(string id);
        #endregion

        #region category
        Task<List<AttributeCategoryDto>> GetAttributeCategoryList();
        Task<bool> CreateAttributeCategory(AttributeCategoryCreateRequest request);
        Task<bool> UpdateAttributeCategoryOrder(Guid categoryid, string attibuteid, int order);
        Task<bool> DeleteAttributeCategory(Guid categoryid, string attibuteid);
        #endregion

        #region order
        Task<List<AttributeOrderContactDto>> GetAttributeOrderList();

        Task<bool> CreateAttributeOrder(AttributeOrderContactCreateRequest request);

        Task<bool> UpdateAttributeOrderOrder(string attibuteid, int order);

        Task<bool> DeleteAttributeOrder(string attibuteid);
        #endregion

        #region contact
        Task<List<AttributeOrderContactDto>> GetAttributeContactList();

        Task<bool> CreateAttributeContact(AttributeOrderContactCreateRequest request);

        Task<bool> UpdateAttributeContactOrder(string attibuteid, int order);

        Task<bool> DeleteAttributeContact(string attibuteid);
        #endregion
    }
}
