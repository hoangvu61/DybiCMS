using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                                .Include(e => e.Warehouse)
                                .Include(e => e.FromSupplier)
                                .Include(e => e.FromFactory)
                                .Include(e => e.FromWarehouse)
                                .Include(e => e.FromOrder)
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
            var input = await _context.WarehouseInputs
                                .Include(e => e.Warehouse)
                                .Include(e => e.FromSupplier)
                                .Include(e => e.FromFactory)
                                .Include(e => e.FromWarehouse)
                                .Include(e => e.FromOrder)
                                .Include(e => e.Debt)
                                .Include(e => e.Products)
                                .Where(e => e.Warehouse.CompanyId == companyId && e.Id == inputId)
                                .FirstOrDefaultAsync();
            return input;
        }
        public async Task<bool> CheckExistInputCode(Guid companyId, string inputCode)
        {
            var check = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && e.InputCode == inputCode)
                                .AnyAsync();
            return check;
        }
        public async Task<bool> CheckExistProducts(Guid companyId, Guid inputId)
        {
            var check = await _context.WarehouseInputProducts.Where(e => e.Input.Warehouse.CompanyId == companyId && e.InputId == inputId)
                                .AnyAsync();
            return check;
        }
        public async Task<WarehouseInput> CreateInput(WarehouseInput input)
        {
            _context.WarehouseInputs.Add(input);

            if (input.Debt != null && input.FromSupplier != null)
            {
                var currentDebt = await _context.DebtSuppliers.Where(e => e.SupplierId == input.FromSupplier.SourceId)
                    .OrderByDescending(e => e.CreateDate)
                    .Select(e => e.TotalDebt)
                    .FirstOrDefaultAsync();
                var debt = new DebtSupplier();
                debt.SupplierId = input.FromSupplier.SourceId;
                debt.Type = 1;
                debt.CreateDate = DateTime.Now;
                debt.Price = input.Debt.Debit;
                debt.TotalDebt = currentDebt + input.Debt.Debit;
                _context.DebtSuppliers.Add(debt);
            }

            await _context.SaveChangesAsync();
            return input;
        }
        public async Task<WarehouseInput> DeleteInput(WarehouseInput warehouseInput)
        {
            _context.WarehouseInputs.Remove(warehouseInput);
            await _context.SaveChangesAsync();
            return warehouseInput;
        }


        public async Task<List<WarehouseInputProduct>> GetInputProducts(Guid companyId, Guid inputId)
        {
            var query = _context.WarehouseInputProducts
                .Include(e => e.Product).ThenInclude(e => e.Category)
                .Include(e => e.Codes)
                .Where(e => e.Input.Warehouse.CompanyId == companyId && e.InputId == inputId);
            return await query.ToListAsync();
        }
        public async Task<WarehouseInputProduct> GetInputProduct(Guid companyId, Guid inputId, Guid productId)
        {
            var query = _context.WarehouseInputProducts
                .Include(e => e.Product).ThenInclude(e => e.Category)
                .Include(e => e.Codes)
                .Where(e => e.Input.Warehouse.CompanyId == companyId && e.InputId == inputId && e.ProductId == productId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> CheckExistProductInput(Guid companyId, Guid inputId, Guid productId)
        {
            var check = await _context.WarehouseInputProducts.Where(e => e.Input.Warehouse.CompanyId == companyId 
                                                                    && e.InputId == inputId 
                                                                    && e.ProductId == productId)
                                .AnyAsync();
            return check;
        }
        public async Task<WarehouseInputProduct> CreateInputProduct(WarehouseInputProduct product)
        {
            var input = await _context.WarehouseInputs.Where(e => e.Id == product.InputId).FirstOrDefaultAsync();
            if (input == null) return null;

            _context.WarehouseInputProducts.Add(product);

            var warehouseInventory = await _context.WarehouseInventories.FirstOrDefaultAsync(e => e.WarehouseId == input.WarehouseId && e.ProductId == product.ProductId);
            if (warehouseInventory == null)
            {
                warehouseInventory = new WarehouseInventory
                {
                    WarehouseId = input.WarehouseId,
                    ProductId = product.ProductId,
                    InventoryNumber = product.Quantity,
                };
                _context.WarehouseInventories.Add(warehouseInventory);
            }
            else
            {
                warehouseInventory.InventoryNumber += product.Quantity;
                _context.WarehouseInventories.Update(warehouseInventory);
            }    

            var warehouseInputInventory = await _context.WarehouseInputInventories.FirstOrDefaultAsync(e => e.InputId == product.InputId && e.ProductId == product.ProductId);
            if (warehouseInputInventory == null)
            {
                warehouseInputInventory = new WarehouseInputInventory
                {
                    InputId = product.InputId,
                    ProductId = product.ProductId,
                    InventoryNumber = product.Quantity
                };
                _context.WarehouseInputInventories.Add(warehouseInputInventory);
            }
            else
            {
                warehouseInputInventory.InventoryNumber += product.Quantity;
                _context.WarehouseInputInventories.Update(warehouseInputInventory);
            }    

            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<int> DeleteInputProduct(WarehouseInputProduct product)
        {
            try
            {
                var input = await _context.WarehouseInputs.Where(e => e.Id == product.InputId).FirstOrDefaultAsync();
                if (input == null) return 1;

                var warehouseInventory = await _context.WarehouseInventories.FirstOrDefaultAsync(e => e.WarehouseId == input.WarehouseId && e.ProductId == product.ProductId);
                if (warehouseInventory == null) return 2;
                if (warehouseInventory.InventoryNumber < product.Quantity) return 3;
                warehouseInventory.InventoryNumber -= product.Quantity;
                _context.WarehouseInventories.Update(warehouseInventory);

                var warehouseInputInventory = await _context.WarehouseInputInventories.FirstOrDefaultAsync(e => e.InputId == product.InputId && e.ProductId == product.ProductId);
                if (warehouseInputInventory == null) return 4;
                if (warehouseInputInventory.InventoryNumber < product.Quantity) return 5;
                warehouseInputInventory.InventoryNumber -= product.Quantity;
                _context.WarehouseInputInventories.Update(warehouseInputInventory);

                _context.WarehouseInputProducts.Remove(product);

                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<WarehouseInputProductCode>> GetInputProductCodes(Guid companyId, Guid inputId, Guid productId)
        {
            var query = _context.WarehouseInputProductCodes
                .Where(e => e.Product.Input.Warehouse.CompanyId == companyId && e.InputId == inputId && e.ProductId == productId);
            return await query.ToListAsync();
        }
        public async Task<WarehouseInputProductCode> GetInputProductCode(Guid companyId, Guid inputId, Guid productId, string code)
        {
            var query = _context.WarehouseInputProductCodes
                .Where(e => e.Product.Input.Warehouse.CompanyId == companyId && e.InputId == inputId && e.ProductId == productId && e.ProductCode == code);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> CheckExistProductInputCode(Guid companyId, Guid productId, string code)
        {
            var check = await _context.WarehouseInputProductCodes.Where(e => e.Product.Input.Warehouse.CompanyId == companyId
                                                                    && e.ProductId == productId
                                                                    && e.ProductCode == code)
                                .AnyAsync();
            return check;
        }
        public async Task<int> CreateInputProductCode(WarehouseInputProductCode productCode)
        {
            try
            {
                var input = await _context.WarehouseInputs.Where(e => e.Id == productCode.InputId).FirstOrDefaultAsync();
                if (input == null) return 1;

                var product = await _context.WarehouseInputProducts.Where(e => e.ProductId == productCode.ProductId).FirstOrDefaultAsync();
                if (input == null) return 2;

                _context.WarehouseInputProductCodes.Add(productCode);

                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> DeleteInputProductCode(WarehouseInputProductCode productCode)
        {
            try
            {
                var input = await _context.WarehouseInputs.Where(e => e.Id == productCode.InputId).FirstOrDefaultAsync();
                if (input == null) return 1;

                var product = await _context.WarehouseInputProducts.Where(e => e.ProductId == productCode.ProductId).FirstOrDefaultAsync();
                if (input == null) return 2;

                _context.WarehouseInputProductCodes.Remove(productCode);

                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
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

        #region inventory
        public async Task<PagedList<WarehouseInventory>> GetInventories(Guid companyId, WarehouseInventorySearch search)
        {
            var query = _context.WarehouseInventories
                                .Include(e => e.Warehouse)
                                .Include(e => e.Product).ThenInclude(e => e.Item).ThenInclude(e => e.ItemLanguages)
                                .Include(e => e.Product).ThenInclude(e => e.Category).ThenInclude(e => e.Item).ThenInclude(e => e.ItemLanguages)
                                .Where(e => e.Warehouse.CompanyId == companyId);

            if (search.WarehouseId != null && search.WarehouseId != Guid.Empty)
            {
                query = query.Where(e => e.WarehouseId == search.WarehouseId);
            }

            if (search.CategoryId != null && search.CategoryId != Guid.Empty)
            {
                query = query.Where(e => e.Product.CategoryId == search.CategoryId);
            }

            if (!string.IsNullOrEmpty(search.Key))
            {
                var key = search.Key.Trim();
                query = query.Where(e => (e.Product.Code != null && e.Product.Code.Contains(key)) || e.Product.Item.ItemLanguages.Any(l => l.Title.Contains(key)));
            }

            if(search.IsAlertEmpty)
            {
                var config = await _context.WarehouseConfigs.Where(e => e.CompanyId == companyId && e.Key == "ThongBaoNhapHangKhiConBaoNhieuSanPham").Select(e => e.Value).FirstOrDefaultAsync();
                var ThongBaoNhapHangKhiConBaoNhieuSanPham = int.Parse(config ?? "0");
                query = query.Where(e => e.InventoryNumber <= ThongBaoNhapHangKhiConBaoNhieuSanPham);
            }    

            var count = await query.CountAsync();

            var data = await query.OrderBy(e => e.Product.Item.CreateDate)
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();
            return new PagedList<WarehouseInventory>(data, count, search.PageNumber, search.PageSize);
        }
        public async Task<List<WarehouseInputInventory>> GetInventories(Guid companyId, Guid productId)
        {
            var data = await _context.WarehouseInputInventories
                                .Include(e => e.Input).ThenInclude(e => e.Warehouse)
                                .Include(e => e.Input).ThenInclude(e => e.FromSupplier)
                                .Include(e => e.Input).ThenInclude(e => e.FromFactory)
                                .Include(e => e.Input).ThenInclude(e => e.FromWarehouse)
                                .Include(e => e.Input).ThenInclude(e => e.FromOrder)
                                .Include(e => e.Input).ThenInclude(e => e.Products)
                                .Where(e => e.Input.Warehouse.CompanyId == companyId && e.ProductId == productId && e.InventoryNumber > 0)
                                .OrderByDescending(e => e.Input.CreateDate)
                                .ToListAsync();
            return data;
        }
        #endregion
    }
}
