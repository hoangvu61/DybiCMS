using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Web.Api.Data;
using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public class SEORepository : ISEORepository
    {
        private readonly WebDbContext _context;
        public SEORepository(WebDbContext context)
        {
            _context = context;
        }

        public async Task<List<SEO>> GetAllSEOs(Guid companyId)
        {
            var seos = await _context.SEOs.Where(e => e.CompanyId == companyId && (e.Item == null || e.Item.IsPublished)).ToListAsync();
            return seos;
        }

        public async Task<SEO> GetSEO(Guid companyId, Guid itemId, string language)
        {
            var seo = await _context.SEOs.FirstOrDefaultAsync(e => e.ItemId == itemId && e.CompanyId == companyId && e.LanguageCode == language);
            return seo;
        }
        public async Task<List<SEO>> GetSEOWithoutItems(Guid companyId)
        {
            var seos = await _context.SEOs.Where(e => e.CompanyId == companyId && e.ItemId == null).ToListAsync();
            return seos;
        }

        public async Task<SEO> GetSEOWithoutItem(Guid companyId, Guid id)
        {
            var seo = await _context.SEOs.Where(e => e.Id == id && e.CompanyId == companyId).FirstOrDefaultAsync();
            return seo;
        }

        public async Task<bool> CheckExist(Guid companyId, string seoUrl)
        {
            var check = await _context.SEOs.AnyAsync(e => e.CompanyId == companyId && e.SeoUrl == seoUrl);
            return check;
        }
        public async Task<bool> CheckExist(Guid companyId, string seoUrl, string url)
        {
            var check = await _context.SEOs.AnyAsync(e => e.CompanyId == companyId && (e.SeoUrl == seoUrl || e.Url == url));
            return check;
        }

        public async Task<bool> CheckExist(Guid companyId, Guid id, string seoUrl, string url)
        {
            var check = await _context.SEOs.AnyAsync(e => e.CompanyId == companyId && e.Id != id && (e.SeoUrl == seoUrl || e.Url == url));
            return check;
        }

        public async Task<SEO> CreateSEO(SEO sEO)
        {
            _context.SEOs.Add(sEO);
            await _context.SaveChangesAsync();
            return sEO;
        }

        public async Task<SEO> UpdateSEO(SEO seo)
        {
            _context.SEOs.Update(seo);
            await _context.SaveChangesAsync();
            return seo;
        }

        public async Task<bool> DeleteSEO(List<SEO> seos)
        {
            foreach (var seo in seos)
            {
                _context.SEOs.Remove(seo);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSEO(SEO seo)
        {
            _context.SEOs.Remove(seo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
