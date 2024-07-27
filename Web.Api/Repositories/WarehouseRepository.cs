using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly WebDbContext _context;
        public WarehouseRepository(WebDbContext context)
        {
            _context = context;
        }

        #region config
        public async Task<List<Config>> GetConfigDefaults()
        {
            var configs = await _context.Configs.ToListAsync();
            return configs;
        }

        public async Task<List<WarehouseConfig>> GetConfigs(Guid companyId)
        {
            var configs = await _context.WarehouseConfigs.Where(e => e.CompanyId == companyId).ToListAsync();
            return configs;
        }
        public async Task<string?> GetConfig(Guid companyId, string key)
        {
            var config = await _context.WarehouseConfigs.Where(e => e.CompanyId == companyId && e.Key == key).Select(e => e.Value).FirstOrDefaultAsync();
            return config;
        }

        public async Task<WarehouseConfig> SetConfig(Guid companyId, string key, string? value)
        {
            var config = await _context.WarehouseConfigs.FirstOrDefaultAsync(e => e.CompanyId == companyId && e.Key == key);
            if (config == null)
            {
                config = new WarehouseConfig
                {
                    CompanyId = companyId,
                    Key = key,
                };
                _context.WarehouseConfigs.Add(config);
            }
            else _context.WarehouseConfigs.Update(config);

            config.Value = value?.Trim();

            await _context.SaveChangesAsync();
            return config;
        }
        #endregion

        #region warehouse
        public async Task<List<WarehouseInput>> GetInputs(Guid companyId)
        {
            var configs = await _context.WarehouseInputs
                                .Include(e => e.Supplier)
                                .Include(e => e.Debt)
                                .Include(e => e.Products)
                                .Where(e => e.Warehouse.CompanyId == companyId)
                                .ToListAsync();
            return configs;
        }

        public async Task<WarehouseInput?> GetInput(Guid companyId, Guid inputId)
        {
            var config = await _context.WarehouseInputs
                                .Include(e => e.Debt)
                                .Include(e => e.Products)
                                .Where(e => e.Warehouse.CompanyId == companyId && e.Id == inputId)
                                .FirstOrDefaultAsync();
            return config;
        }

        public async Task<ThirdParty> DeleteInput(ThirdParty thirdParty)
        {
            _context.ThirdParties.Remove(thirdParty);
            await _context.SaveChangesAsync();
            return thirdParty;
        }
        #endregion
    }
}
