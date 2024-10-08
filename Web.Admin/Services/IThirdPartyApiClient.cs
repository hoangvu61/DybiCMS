using Web.Models;

namespace Web.Admin.Services
{
    public interface IThirdPartyApiClient
    {
        #region ThirdParty
        Task<List<ThirdPartyDto>> GetList();
        Task<ThirdPartyDto> GetById(Guid id);
        Task<bool> Create(ThirdPartyDto request);
        Task<bool> Update(ThirdPartyDto request);
        Task<bool> Delete(Guid id);
        #endregion
    }
}
