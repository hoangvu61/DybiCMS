using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models;
using Web.Models.SeedWork;

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
        public async Task<List<Warehouse>> GetWarehouses(Guid companyId)
        {
            var query = _context.Warehouses.Where(e => e.CompanyId == companyId);
            return await query.ToListAsync();
        }
        public async Task<Warehouse?> GetWarehouse(Guid id)
        {
            var query = _context.Warehouses.Where(e => e.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Warehouse> CreateWarehouse(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }
        public async Task<Warehouse> UpdateWarehouse(Warehouse warehouse)
        {
            _context.Warehouses.Update(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }
        public async Task<Warehouse> DeleteWarehouse(Warehouse warehouse)
        {
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }


        public async Task<PagedList<WarehouseInput>> GetInputs(Guid companyId, WarehouseInputSearch search)
        {
            var query = _context.WarehouseInputs
                                .Include(e => e.FromSupplier)
                                .Include(e => e.FromFactory)
                                .Include(e => e.FromWarehouse)
                                .Include(e => e.Debt)
                                .Include(e => e.Products)
                                .Where(e => e.Warehouse.CompanyId == companyId);

            if (!string.IsNullOrEmpty(search.Code))
            {
                var key = search.Code.Trim();
                query = query.Where(e => (e.InputCode != null && e.InputCode.Contains(key)));
            }
            if (search.FromDate != null) query = query.Where(e => search.FromDate <= e.CreateDate);
            if (search.ToDate != null) query = query.Where(e => e.CreateDate <= search.ToDate);
            if (search.WarehouseId != null) query = query.Where(e => e.Warehouse.Id == search.WarehouseId);
            if (search.SupplyerId != null) query = query.Where(e => e.FromSupplier != null && e.FromSupplier.SourceId == search.SupplyerId);
            if (search.FactoryId != null) query = query.Where(e => e.FromFactory != null && e.FromFactory.FactoryId == search.FactoryId);
            if (search.FromWarehouseId != null) query = query.Where(e => e.FromWarehouse != null && e.FromWarehouse.WarehouseId == search.FromWarehouseId);

            var count = await query.CountAsync();

            var data = await query.OrderBy(e => e.CreateDate)
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();
            return new PagedList<WarehouseInput>(data, count, search.PageNumber, search.PageSize);
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
        public async Task<WarehouseInput> DeleteInput(WarehouseInput warehouseInput)
        {
            _context.WarehouseInputs.Remove(warehouseInput);
            await _context.SaveChangesAsync();
            return warehouseInput;
        }
        #endregion

        #region factory
        public async Task<List<WarehouseFactory>> GetFactories(Guid companyId)
        {
            var query = _context.WarehouseFactorys.Where(e => e.CompanyId == companyId);
            return await query.ToListAsync();
        }
        public async Task<WarehouseFactory?> GetFactory(Guid id)
        {
            var query = _context.WarehouseFactorys.Where(e => e.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<WarehouseFactory> CreateFactory(WarehouseFactory factory)
        {
            _context.WarehouseFactorys.Add(factory);
            await _context.SaveChangesAsync();
            return factory;
        }
        public async Task<WarehouseFactory> UpdateFactory(WarehouseFactory factory)
        {
            _context.WarehouseFactorys.Update(factory);
            await _context.SaveChangesAsync();
            return factory;
        }
        public async Task<WarehouseFactory> DeleteFactory(WarehouseFactory factory)
        {
            _context.WarehouseFactorys.Remove(factory);
            await _context.SaveChangesAsync();
            return factory;
        }
        #endregion

        #region supplier
        public async Task<List<WarehouseSupplier>> GetSuppliers(Guid companyId)
        {
            var query = _context.WarehouseSuppliers.Where(e => e.CompanyId == companyId);
            return await query.ToListAsync();
        }
        public async Task<WarehouseSupplier?> GetSupplier(Guid id)
        {
            var query = _context.WarehouseSuppliers.Where(e => e.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<WarehouseSupplier> CreateSupplier(WarehouseSupplier supplier)
        {
            _context.WarehouseSuppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }
        public async Task<WarehouseSupplier> UpdateSupplier(WarehouseSupplier supplier)
        {
            _context.WarehouseSuppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }
        public async Task<WarehouseSupplier> DeleteSupplier(WarehouseSupplier factory)
        {
            _context.WarehouseSuppliers.Remove(factory);
            await _context.SaveChangesAsync();
            return factory;
        }
        #endregion


    }
}
