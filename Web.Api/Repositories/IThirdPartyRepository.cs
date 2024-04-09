using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public interface IThirdPartyRepository
    {
        Task<List<ThirdParty>> GetThirdParties(Guid companyId);
        Task<ThirdParty> GetThirdParty(Guid id);
        Task<ThirdParty> CreateThirdParty(ThirdParty thirdParty);
        Task<ThirdParty> UpdateThirdParty(ThirdParty thirdParty);
        Task<ThirdParty> DeleteThirdParty(ThirdParty thirdParty);
    }
}
