using Web.Models;

namespace Web.Admin.Services
{
    public interface IMenuApiClient
    {
        #region ThirdParty
        Task<List<MenuDto>> GetList();
        Task<MenuDto> GetById(Guid id);
        Task<bool> Create(MenuCreateRequest request);
        Task<bool> UpdateOrder(Guid id, int order);
        Task<bool> Update(MenuDto request);
        Task<bool> Delete(Guid id);
        #endregion
    }
}
