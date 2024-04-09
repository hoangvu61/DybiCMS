using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly WebDbContext _context;
        public MenuRepository(WebDbContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> GetMenus(Guid companyId)
        {
            var query = _context.Menus
                .Include(e => e.Item)
                .Include(e => e.Item.ItemLanguages)
                .Where(e => e.Item.CompanyId == companyId)
                .OrderBy(e => e.Order);
            return await query.ToListAsync();
        }

        public async Task<Menu> GetMenu(Guid id)
        {
            var query = _context.Menus.Where(e => e.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Menu> CreateMenu(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<Menu> UpdateMenu(Menu menu)
        {
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<Menu> DeleteMenu(Menu menu)
        {
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return menu;
        }
    }
}
