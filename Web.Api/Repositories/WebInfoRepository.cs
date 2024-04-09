using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Web.Api.Data;
using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public class WebInfoRepository : IWebInfoRepository
    {
        private readonly WebDbContext _context;
        public WebInfoRepository(WebDbContext context)
        {
            _context = context;
        }
        public async Task<WebInfo> GetWebInfo(Guid companyId, string language)
        {
            var web = await _context.WebInfos.FirstOrDefaultAsync(e =>e.CompanyId == companyId && e.LanguageCode == language);
            return web;
        }
        public async Task<WebInfo> CreateWebInfo(WebInfo web)
        {
            _context.WebInfos.Add(web);
            await _context.SaveChangesAsync();
            return web;
        }
        public async Task<WebInfo> UpdateWebInfo(WebInfo web)
        {
            _context.WebInfos.Update(web);
            await _context.SaveChangesAsync();
            return web;
        }
    }
}
