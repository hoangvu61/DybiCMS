using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetMenus(Guid companyId);
        Task<Menu> GetMenu(Guid id);
        Task<Menu> CreateMenu(Menu menu);
        Task<Menu> UpdateMenu(Menu menu);
        Task<Menu> DeleteMenu(Menu menu);
    }
}
