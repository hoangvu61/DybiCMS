using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public class ThirdPartyRepository : IThirdPartyRepository
    {
        private readonly WebDbContext _context;
        public ThirdPartyRepository(WebDbContext context)
        {
            _context = context;
        }

        public async Task<ThirdParty> CreateThirdParty(ThirdParty thirdParty)
        {
            _context.ThirdParties.Add(thirdParty);
            await _context.SaveChangesAsync();
            return thirdParty;
        }

        public async Task<ThirdParty> DeleteThirdParty(ThirdParty thirdParty)
        {
            _context.ThirdParties.Remove(thirdParty);
            await _context.SaveChangesAsync();
            return thirdParty;
        }

        public async Task<List<ThirdParty>> GetThirdParties(Guid companyId)
        {
            var query = _context.ThirdParties.Where(e => e.CompanyId == companyId);
            return await query.ToListAsync();
        }

        public async Task<ThirdParty> GetThirdParty(Guid id)
        {
            var query = _context.ThirdParties.Where(e => e.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<ThirdParty> UpdateThirdParty(ThirdParty thirdParty)
        {
            _context.ThirdParties.Update(thirdParty);
            await _context.SaveChangesAsync();
            return thirdParty;
        }
    }
}
