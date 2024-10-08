using Web.Models;

namespace Web.App.Services
{
    public interface IAttributeApiClient
    {
        #region attribute
        Task<List<AttributeOrderConfigDto>> GetAttributeList();
        Task<AttributeOrderConfigDetailDto> GetAttributeById(string id);
        Task<bool> CreateAttribute(AttributeOrderConfigDetailDto request);
        Task<bool> UpdateAttribute(AttributeOrderConfigDetailDto request);
        Task<bool> UpdateAttributeOrderOrder(string attibuteid, int order);
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
    }
}
