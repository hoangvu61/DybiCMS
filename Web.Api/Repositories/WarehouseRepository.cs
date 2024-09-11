using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Polly;
using System.ComponentModel.Design;
using System.Net;
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
            var query = _context.Warehouses.Include(e => e.Inputs).Where(e => e.Id == id);
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


        public async Task<PagedList<WarehouseInput>> GetInputs(Guid companyId, WarehouseIOSearch search)
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
            if (search.FromOrToWarehouseId != null) query = query.Where(e => e.FromWarehouse != null && e.FromWarehouse.WarehouseId == search.FromOrToWarehouseId);

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
        public async Task<bool> CheckExistProductInInput(Guid companyId, Guid inputId, Guid productId)
        {
            var query = _context.WarehouseInputProducts.Where(e => e.Input.Warehouse.CompanyId == companyId && e.InputId == inputId);
            if (productId != Guid.Empty)
            {
                query = query.Where(e => e.ProductId == productId);
            }
            var check = await query.AnyAsync();
            return check;
        }
        public async Task<int> CreateInput(WarehouseInput input)
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
            return 0;
        }
        public async Task<int> DeleteInput(Guid companyId, Guid inputId)
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
            if (input == null) return 1;
            if (input.Products.Count > 0) return 2;
            if (input.Debt != null && input.FromSupplier != null)
            {
                var debts = await _context.DebtSuppliers.Where(e => e.SupplierId == input.FromSupplier.SourceId).ToListAsync();
                _context.DebtSuppliers.RemoveRange(debts);
            }

            var inventories = await _context.WarehouseInputInventories.Where(e => e.InputId == input.Id).ToListAsync();
            if (inventories != null) _context.WarehouseInputInventories.RemoveRange(inventories);

            _context.WarehouseInputs.Remove(input);
            await _context.SaveChangesAsync();
            return 0;
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
        public async Task<int> CreateInputProduct(WarehouseInputProduct product)
        {
            var input = await _context.WarehouseInputs.Where(e => e.Id == product.InputId).FirstOrDefaultAsync();
            if (input == null) return 1;

            var item = await _context.ItemProducts.FirstOrDefaultAsync(e => e.ItemId == product.ProductId);
            if (item == null) return 2;

            if (product.Quantity == 1 && !string.IsNullOrEmpty(item.Code)) 
                product.Codes = new List<WarehouseInputProductCode> { new WarehouseInputProductCode { ProductCode = item.Code } };
            
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
            return 0;
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
                .Where(e => e.InputProduct.Input.Warehouse.CompanyId == companyId && e.InputId == inputId && e.ProductId == productId);
            return await query.ToListAsync();
        }
        public async Task<WarehouseInputProductCode> GetInputProductCode(Guid companyId, Guid inputId, Guid productId, string code)
        {
            var query = _context.WarehouseInputProductCodes
                .Where(e => e.InputProduct.Input.Warehouse.CompanyId == companyId && e.InputId == inputId && e.ProductId == productId && e.ProductCode == code);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> CheckExistProductInputCode(Guid companyId, Guid productId, string code)
        {
            var check = await _context.WarehouseInputProductCodes.Where(e => e.InputProduct.Input.Warehouse.CompanyId == companyId
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


        public async Task<PagedList<WarehouseOutput>> GetOutputs(Guid companyId, WarehouseIOSearch search)
        {
            var query = _context.WarehouseOutputs
                                .Include(e => e.Warehouse)
                                .Include(e => e.ToSupplier)
                                .Include(e => e.ToFactory)
                                .Include(e => e.ToWarehouse)
                                .Include(e => e.ToOrder)
                                .Include(e => e.Products)
                                .Where(e => e.Warehouse.CompanyId == companyId);

            if (search.FromDate != null) query = query.Where(e => search.FromDate <= e.CreateDate);
            if (search.ToDate != null) query = query.Where(e => e.CreateDate <= search.ToDate);
            if (search.WarehouseId != null) query = query.Where(e => e.Warehouse.Id == search.WarehouseId);
            if (search.SupplyerId != null) query = query.Where(e => e.ToSupplier != null && e.ToSupplier.SourceId == search.SupplyerId);
            if (search.FactoryId != null) query = query.Where(e => e.ToFactory != null && e.ToFactory.FactoryId == search.FactoryId);
            if (search.FromOrToWarehouseId != null) query = query.Where(e => e.ToWarehouse != null && e.ToWarehouse.WarehouseId == search.FromOrToWarehouseId);

            var count = await query.CountAsync();

            var data = await query.OrderBy(e => e.CreateDate)
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();
            return new PagedList<WarehouseOutput>(data, count, search.PageNumber, search.PageSize);
        }
        public async Task<WarehouseOutput?> GetOutput(Guid companyId, Guid inputId)
        {
            var output = await _context.WarehouseOutputs
                                .Include(e => e.Warehouse)
                                .Include(e => e.ToSupplier)
                                .Include(e => e.ToFactory)
                                .Include(e => e.ToWarehouse)
                                .Include(e => e.ToOrder)
                                .Include(e => e.Products)
                                .Where(e => e.Warehouse.CompanyId == companyId && e.Id == inputId)
                                .FirstOrDefaultAsync();
            return output;
        }
        public async Task<Order> GetOutputOrder(Guid companyId, Guid orderId)
        {
            var query = _context.WarehouseOutputToOrders
                .Include(e => e.Order).ThenInclude(e => e.Products)
                .Where(e => e.Output.Warehouse.CompanyId == companyId && e.OrderId == orderId)
                .Select(e => e.Order);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> CheckExistProductInOutputs(Guid companyId, Guid outputId, Guid productId)
        {
            var query = _context.WarehouseOutputProducts.Where(e => e.Output.Warehouse.CompanyId == companyId && e.OutputId == outputId);
            if (productId != Guid.Empty)
            {
                query = query.Where(e => e.ProductId == productId);
            }
            var check = await query.AnyAsync();
            return check;
        }
        public async Task<int> CreateOutput(Guid companyId, WarehouseOutput output)
        {
            _context.WarehouseOutputs.Add(output);

            if (output.Type == 1)
            {
                var phuongPhapTinhGiaXuatKho = await _context.WarehouseConfigs.Where(e => e.CompanyId == companyId && e.Key == "PhuongPhapTinhGiaXuatKho").Select(e => e.Value).FirstOrDefaultAsync();
                if (phuongPhapTinhGiaXuatKho == null) return 1;
                if (phuongPhapTinhGiaXuatKho != "2")
                {
                    foreach (var product in output.Products)
                    {
                        var warehouseInventory = await _context.WarehouseInventories.FirstOrDefaultAsync(e => e.WarehouseId == output.WarehouseId && e.ProductId == product.ProductId);
                        if (warehouseInventory == null) return 2;
                        else if (warehouseInventory.InventoryNumber < product.Quantity) return 3;

                        warehouseInventory.InventoryNumber -= product.Quantity;
                        _context.WarehouseInventories.Update(warehouseInventory);

                        var warehouseInputInventories = await _context.WarehouseInputInventories.Where(e => e.Input.WarehouseId == output.Id && e.ProductId == product.ProductId && e.InventoryNumber > 0).ToListAsync();
                        if (warehouseInputInventories == null || warehouseInputInventories.Count == 0) return 4;

                        var inputIds = warehouseInputInventories.Select(e => e.InputId).ToList();
                        var inputProducts = await _context.WarehouseInputProducts.Where(e => inputIds.Contains(e.InputId)).OrderBy(e => e.Input.CreateDate).ToListAsync();

                        foreach (var inputProduct in inputProducts)
                        {
                            var detail = new WarehouseOutputProductDetail
                            {
                                OutputId = product.OutputId,
                                InputId = inputProduct.InputId,
                                ProductId = inputProduct.ProductId,
                                Price = inputProduct.Price
                            };

                            var warehouseInputInventory = warehouseInputInventories.First(e => e.InputId == inputProduct.InputId);
                            if (warehouseInputInventory.InventoryNumber < product.Quantity)
                            {
                                detail.Quantity = warehouseInputInventory.InventoryNumber;

                                _context.WarehouseInputInventories.Remove(warehouseInputInventory);
                            }
                            else
                            {
                                detail.Quantity = product.Quantity;
                                warehouseInputInventory.InventoryNumber -= product.Quantity;
                                _context.WarehouseInputInventories.Update(warehouseInputInventory);
                                break;
                            }

                            product.Details.Add(detail);
                        }
                    }
                }
            }    

            await _context.SaveChangesAsync();
            return 0;
        }
        public async Task<WarehouseOutput> DeleteOutput(WarehouseOutput warehouseOutput)
        {
            _context.WarehouseOutputs.Remove(warehouseOutput);
            await _context.SaveChangesAsync();
            return warehouseOutput;
        }

        public async Task<List<WarehouseOutputProductDetail>> GetOutputProducts(Guid companyId, Guid outputId)
        {
            var query = _context.WarehouseOutputProductDetails
                .Include(e => e.ProductInput).ThenInclude(e => e.Product).ThenInclude(e => e.Category)
                .Include(e => e.ProductInput).ThenInclude(e => e.Input)
                .Include(e => e.ProducOutput).ThenInclude(e => e.Codes)
                .Where(e => e.ProducOutput.Output.Warehouse.CompanyId == companyId && e.OutputId == outputId);
            return await query.ToListAsync();
        }
        public async Task<WarehouseOutputProduct> GetOutputProduct(Guid companyId, Guid outputId, Guid productId)
        {
            var query = _context.WarehouseOutputProducts
                .Include(e => e.Product).ThenInclude(e => e.Category)
                .Include(e => e.Codes)
                .Include(e => e.Details)
                .Where(e => e.Output.Warehouse.CompanyId == companyId && e.OutputId == outputId && e.ProductId == productId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<int> CreateOutputProduct(Guid companyId, WarehouseOutputProduct product)
        {
            var output = await _context.WarehouseOutputs.Where(e => e.Id == product.OutputId).FirstOrDefaultAsync();
            if (output == null) return 1;

            var item = await _context.ItemProducts.FirstOrDefaultAsync(e => e.ItemId == product.ProductId);
            if (item == null) return 2;

            var warehouseInventory = await _context.WarehouseInventories.FirstOrDefaultAsync(e => e.WarehouseId == output.WarehouseId && e.ProductId == product.ProductId);
            if (warehouseInventory == null) return 3;
            else if (warehouseInventory.InventoryNumber < product.Quantity) return 4;

            var phuongPhapTinhGiaXuatKho = await _context.WarehouseConfigs.Where(e => e.CompanyId == companyId && e.Key == "PhuongPhapTinhGiaXuatKho").Select(e => e.Value).FirstOrDefaultAsync();
            if (phuongPhapTinhGiaXuatKho != "2")
            {
                var warehouseInputInventories = await _context.WarehouseInputInventories.Where(e => e.ProductId == product.ProductId && e.InventoryNumber > 0).ToListAsync();
                if(warehouseInputInventories == null || warehouseInputInventories.Count == 0) return 5;

                var inputIds = warehouseInputInventories.Select(e => e.InputId).ToList();
                var inputProducts = await _context.WarehouseInputProducts.Where(e => inputIds.Contains(e.InputId)).OrderBy(e => e.Input.CreateDate).ToListAsync();

                foreach(var inputProduct in inputProducts)
                {
                    var detail = new WarehouseOutputProductDetail
                    {
                        OutputId = product.OutputId,
                        InputId = inputProduct.InputId,
                        ProductId = inputProduct.ProductId,
                        Price = inputProduct.Price
                    };

                    var warehouseInputInventory = warehouseInputInventories.First(e => e.InputId == inputProduct.InputId);
                    if (warehouseInputInventory.InventoryNumber < product.Quantity)
                    {
                        detail.Quantity = warehouseInputInventory.InventoryNumber;

                        _context.WarehouseInputInventories.Remove(warehouseInputInventory);
                    }
                    else
                    {
                        detail.Quantity = product.Quantity;

                        warehouseInputInventory.InventoryNumber -= product.Quantity;
                        _context.WarehouseInputInventories.Update(warehouseInputInventory);
                        break;
                    }
                    
                    _context.WarehouseOutputProductDetails.Add(detail);
                }    
            }
            else
            {

            }  

            _context.WarehouseOutputProducts.Add(product);

            warehouseInventory.InventoryNumber -= product.Quantity;
            _context.WarehouseInventories.Update(warehouseInventory);

            await _context.SaveChangesAsync();
            return 0;
        }
        public async Task<int> DeleteOutputProduct(WarehouseOutputProduct product)
        {
            try
            {
                var output = await _context.WarehouseOutputs.Where(e => e.Id == product.OutputId).FirstOrDefaultAsync();
                if (output == null) return 1;

                var warehouseInventory = await _context.WarehouseInventories.FirstOrDefaultAsync(e => e.WarehouseId == output.WarehouseId && e.ProductId == product.ProductId);
                if (warehouseInventory == null)
                {
                    warehouseInventory = new WarehouseInventory
                    {
                        WarehouseId = output.WarehouseId,
                        ProductId = product.ProductId,
                        InventoryNumber = 1
                    };
                    _context.WarehouseInventories.Add(warehouseInventory);
                }
                else
                {
                    warehouseInventory.InventoryNumber += product.Quantity;
                    _context.WarehouseInventories.Update(warehouseInventory);
                }

                foreach(var detail in product.Details)
                {
                    var warehouseInputInventory = await _context.WarehouseInputInventories.FirstOrDefaultAsync(e => e.InputId == detail.InputId && e.ProductId == detail.ProductId);
                    if (warehouseInputInventory == null)
                    {
                        warehouseInputInventory = new WarehouseInputInventory
                        {
                            InputId = detail.InputId,
                            ProductId = detail.ProductId,
                        };
                        _context.WarehouseInputInventories.Add(warehouseInputInventory);
                    }
                    else _context.WarehouseInputInventories.Update(warehouseInputInventory);

                    warehouseInputInventory.InventoryNumber += detail.Quantity;
                }

                product.Details.Clear();
                _context.WarehouseOutputProducts.Remove(product);

                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<WarehouseOutputProductCode>> GetOutputProductCodes(Guid companyId, Guid outputId, Guid productId)
        {
            var query = _context.WarehouseOutputProductCodes
                .Where(e => e.OutProduct.Output.Warehouse.CompanyId == companyId && e.OutputId == outputId && e.ProductId == productId);
            return await query.ToListAsync();
        }
        public async Task<WarehouseOutputProductCode> GetOutputProductCode(Guid companyId, Guid outputId, Guid productId, string code)
        {
            var query = _context.WarehouseOutputProductCodes
                .Where(e => e.OutProduct.Output.Warehouse.CompanyId == companyId && e.OutputId == outputId && e.ProductId == productId && e.ProductCode == code);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> CheckExistProductOutputCode(Guid companyId, Guid productId, string code)
        {
            var check = await _context.WarehouseOutputProductCodes.Where(e => e.OutProduct.Output.Warehouse.CompanyId == companyId
                                                                    && e.ProductId == productId
                                                                    && e.ProductCode == code)
                                .AnyAsync();
            return check;
        }
        public async Task<int> CreateOutputProductCode(Guid companyId, Guid outputId, Guid productId, string productCode)
        {
            try
            {
                var output = await _context.WarehouseOutputs.FirstOrDefaultAsync(e => e.Id == outputId && e.Warehouse.CompanyId == companyId);
                if (output == null) return 1;

                var inputProductCode = await _context.WarehouseInputProductCodes
                                                .Include(e => e.InputProduct)
                                                .Where(e => e.ProductId == productId
                                                            && e.ProductCode == productCode
                                                            && e.InputProduct.Input.WarehouseId == output.WarehouseId
                                                            && e.InputProduct.Input.Warehouse.CompanyId == companyId)
                                                .OrderByDescending(e => e.InputProduct.Input.CreateDate)
                                                .FirstOrDefaultAsync();
                if (inputProductCode == null) return 2;

                var outProductCode = await _context.WarehouseOutputProductCodes.FirstOrDefaultAsync(e => e.ProductCode == productCode && e.OutProduct.OutputId == outputId && e.OutProduct.Output.Warehouse.CompanyId == companyId);
                if (outProductCode != null) return 3;

                var warehouseInventory = await _context.WarehouseInventories
                                            .FirstOrDefaultAsync(e => e.WarehouseId == output.WarehouseId && e.ProductId == productId);
                if (warehouseInventory == null) return 4;
                else if (warehouseInventory.InventoryNumber < 1) return 5;

                var inputInventory = await _context.WarehouseInputInventories
                                            .OrderBy(e => e.Input.CreateDate)
                                            .FirstOrDefaultAsync(e => e.ProductId == productId && e.InventoryNumber > 0);
                if (inputInventory == null) return 6;

                var outputProduct = await _context.WarehouseOutputProducts
                    .Include(e => e.Codes)
                    .Include(e => e.Details)
                    .FirstOrDefaultAsync(e => e.ProductId == productId && e.OutputId == outputId);
                if (outputProduct == null) return 7;

                outputProduct.Quantity += 1;
                outputProduct.Codes.Add(new WarehouseOutputProductCode { ProductCode = productCode, InputId = inputProductCode.InputId });
                var detail = outputProduct.Details.FirstOrDefault(e => e.InputId == inputProductCode.InputId && e.ProductId == inputProductCode.ProductId);
                if (detail == null)
                {
                    detail = new WarehouseOutputProductDetail { InputId = inputProductCode.InputId, Quantity = 1, ProductId = inputProductCode.ProductId, Price = inputProductCode.InputProduct.Price };
                    outputProduct.Details.Add(detail);
                }
                else detail.Quantity += 1;
                _context.WarehouseOutputProducts.Update(outputProduct);

                warehouseInventory.InventoryNumber -= 1;
                if (warehouseInventory.InventoryNumber == 0) _context.WarehouseInventories.Remove(warehouseInventory);
                else _context.WarehouseInventories.Update(warehouseInventory);

                inputInventory.InventoryNumber -= 1;
                if (warehouseInventory.InventoryNumber == 0) _context.WarehouseInputInventories.Remove(inputInventory);
                else _context.WarehouseInputInventories.Update(inputInventory);

                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> CreateOutputProductCode(Guid companyId, Guid outputId, string productCode)
        {
            try
            {
                var output = await _context.WarehouseOutputs.FirstOrDefaultAsync(e => e.Id == outputId && e.Warehouse.CompanyId == companyId);
                if (output == null) return 1;

                var inputProductCodes = await _context.WarehouseInputProductCodes
                                                .Include(e => e.InputProduct)
                                                .Where(e => e.ProductCode == productCode && e.InputProduct.Input.WarehouseId == output.WarehouseId && e.InputProduct.Input.Warehouse.CompanyId == companyId)
                                                .OrderByDescending(e => e.InputProduct.Input.CreateDate)
                                                .ToListAsync();
                if (inputProductCodes == null || inputProductCodes.Count == 0) return 2;
                if (inputProductCodes.Select(e => e.ProductId).Distinct().Count() > 1) return 3;
                var productId = inputProductCodes.Select(e => e.ProductId).First();

                var outProductCode = await _context.WarehouseOutputProductCodes.FirstOrDefaultAsync(e => e.ProductCode == productCode && e.OutProduct.OutputId == outputId && e.OutProduct.Output.Warehouse.CompanyId == companyId);
                if (outProductCode != null) return 4;

                var warehouseInventory = await _context.WarehouseInventories
                                            .FirstOrDefaultAsync(e => e.WarehouseId == output.WarehouseId && e.ProductId == productId);
                if (warehouseInventory == null) return 5;
                else if (warehouseInventory.InventoryNumber < 1) return 6;

                var inputInventory = await _context.WarehouseInputInventories
                                            .OrderBy(e => e.Input.CreateDate)
                                            .FirstOrDefaultAsync(e => e.ProductId == productId && e.InventoryNumber > 0);
                if (inputInventory == null) return 7;
                var productInputPrice = inputProductCodes.Where(e => e.ProductId == productId && e.InputId == inputInventory.InputId).Select(e => e.InputProduct.Price).First();

                var outputProduct = await _context.WarehouseOutputProducts
                    .Include(e => e.Codes)
                    .Include(e => e.Details)
                    .FirstOrDefaultAsync(e => e.ProductId == productId && e.OutputId == outputId);
                if (outputProduct == null)
                {
                    outputProduct = new WarehouseOutputProduct
                    {
                        OutputId = outputId,
                        ProductId = productId,
                        Quantity = 1,
                        Codes = new List<WarehouseOutputProductCode> { new WarehouseOutputProductCode { ProductCode = productCode, InputId = inputInventory.InputId } },
                        Details = new List<WarehouseOutputProductDetail> { new WarehouseOutputProductDetail { InputId = inputInventory.InputId, Quantity = 1, ProductId = productId, Price = productInputPrice } }
                    };
                    _context.WarehouseOutputProducts.Add(outputProduct);
                }
                else
                {
                    outputProduct.Quantity += 1;
                    outputProduct.Codes.Add(new WarehouseOutputProductCode { ProductCode = productCode, InputId = inputInventory.InputId });
                    var detail = outputProduct.Details.FirstOrDefault(e => e.InputId == inputInventory.InputId && e.ProductId == inputInventory.ProductId);
                    if (detail == null)
                    {
                        detail = new WarehouseOutputProductDetail { InputId = inputInventory.InputId, Quantity = 1, ProductId = inputInventory.ProductId, Price = productInputPrice };
                        outputProduct.Details.Add(detail);
                    }
                    else detail.Quantity += 1;
                    _context.WarehouseOutputProducts.Update(outputProduct);
                }

                warehouseInventory.InventoryNumber -= 1;
                if (warehouseInventory.InventoryNumber == 0) _context.WarehouseInventories.Remove(warehouseInventory);
                else _context.WarehouseInventories.Update(warehouseInventory);

                inputInventory.InventoryNumber -= 1;
                if (warehouseInventory.InventoryNumber == 0) _context.WarehouseInputInventories.Remove(inputInventory);
                else _context.WarehouseInputInventories.Update(inputInventory);

                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> DeleteOutputProductCode(Guid companyId, Guid outputId, string productCode)
        {
            try
            {
                var outputProductCodes = await _context.WarehouseOutputProductCodes
                                                .Include(e => e.Input)
                                                .Where(e => e.ProductCode == productCode && e.OutputId == outputId && e.Output.Warehouse.CompanyId == companyId)
                                                .ToListAsync();
                if (outputProductCodes == null || outputProductCodes.Count == 0) return 1;
                if (outputProductCodes.Select(e => e.ProductId).Distinct().Count() > 1) return 2;

                var outputProductCode = outputProductCodes.First();
                
                var outputProduct = await _context.WarehouseOutputProducts
                                        .Include(e => e.Codes)
                                        .Include(e => e.Details)
                                        .Where(e => e.OutputId == outputId && e.ProductId == outputProductCode.ProductId).FirstOrDefaultAsync();
                if (outputProduct == null) return 3;

                // them ton kho
                var warehouseInventory = await _context.WarehouseInventories
                                            .FirstOrDefaultAsync(e => e.WarehouseId == outputProductCode.Input.WarehouseId && e.ProductId == outputProductCode.ProductId);
                if (warehouseInventory == null)
                {
                    warehouseInventory = new WarehouseInventory
                    {
                        WarehouseId = outputProductCode.Input.WarehouseId,
                        ProductId = outputProductCode.ProductId,
                        InventoryNumber = 1
                    };
                    _context.WarehouseInventories.Add(warehouseInventory);
                }
                else
                {
                    warehouseInventory.InventoryNumber += 1;
                    _context.WarehouseInventories.Update(warehouseInventory);
                }

                var inputInventory = await _context.WarehouseInputInventories
                                            .FirstOrDefaultAsync(e => e.InputId == outputProductCode.InputId && e.ProductId == outputProductCode.ProductId);
                if (inputInventory == null)
                {
                    inputInventory = new WarehouseInputInventory
                    {
                        InputId = outputProductCode.InputId,
                        ProductId = outputProductCode.ProductId,
                        InventoryNumber = 1
                    };
                    _context.WarehouseInputInventories.Add(inputInventory);
                }
                else
                {
                    inputInventory.InventoryNumber += 1;
                    _context.WarehouseInputInventories.Update(inputInventory);
                }

                // xoa product neu het code
                if (outputProduct.Codes.Count == 1)
                {
                    outputProduct.Codes.Clear();
                    outputProduct.Details.Clear();
                    _context.WarehouseOutputProducts.Remove(outputProduct);
                }
                else
                {
                    //nếu còn code thì giảm số lượng detail
                    var detail = outputProduct.Details.FirstOrDefault(e => e.ProductId == outputProductCode.ProductId && e.InputId == outputProductCode.InputId);
                    if (detail == null) return 4;

                    detail.Quantity -= 1;
                    _context.WarehouseOutputProductDetails.Update(detail);

                    // xoa code
                    _context.WarehouseOutputProductCodes.Remove(outputProductCode);
                }

                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> DeleteOutputProductCode(Guid companyId, Guid outputId, Guid productId, string productCode)
        {
            try
            {
                var outputProductCodes = await _context.WarehouseOutputProductCodes
                                                .Include(e => e.Input)
                                                .Where(e => e.ProductCode == productCode && e.OutputId == outputId && e.Output.Warehouse.CompanyId == companyId)
                                                .ToListAsync();
                if (outputProductCodes == null || outputProductCodes.Count == 0) return 1;
                if (outputProductCodes.Select(e => e.ProductId).Distinct().Count() > 1) return 2;

                var outputProductCode = outputProductCodes.First();

                var outputProduct = await _context.WarehouseOutputProducts
                                        .Include(e => e.Codes)
                                        .Include(e => e.Details)
                                        .Where(e => e.OutputId == outputId && e.ProductId == outputProductCode.ProductId).FirstOrDefaultAsync();
                if (outputProduct == null) return 3;

                // them ton kho
                var warehouseInventory = await _context.WarehouseInventories
                                            .FirstOrDefaultAsync(e => e.WarehouseId == outputProductCode.Input.WarehouseId && e.ProductId == outputProductCode.ProductId);
                if (warehouseInventory == null)
                {
                    warehouseInventory = new WarehouseInventory
                    {
                        WarehouseId = outputProductCode.Input.WarehouseId,
                        ProductId = outputProductCode.ProductId,
                        InventoryNumber = 1
                    };
                    _context.WarehouseInventories.Add(warehouseInventory);
                }
                else
                {
                    warehouseInventory.InventoryNumber += 1;
                    _context.WarehouseInventories.Update(warehouseInventory);
                }

                var inputInventory = await _context.WarehouseInputInventories
                                            .FirstOrDefaultAsync(e => e.InputId == outputProductCode.InputId && e.ProductId == outputProductCode.ProductId);
                if (inputInventory == null)
                {
                    inputInventory = new WarehouseInputInventory
                    {
                        InputId = outputProductCode.InputId,
                        ProductId = outputProductCode.ProductId,
                        InventoryNumber = 1
                    };
                    _context.WarehouseInputInventories.Add(inputInventory);
                }
                else
                {
                    inputInventory.InventoryNumber += 1;
                    _context.WarehouseInputInventories.Update(inputInventory);
                }

                // xoa product neu het code
                if (outputProduct.Codes.Count == 1)
                {
                    outputProduct.Codes.Clear();
                    outputProduct.Details.Clear();
                    _context.WarehouseOutputProducts.Remove(outputProduct);
                }
                else
                {
                    //nếu còn code thì giảm số lượng detail
                    var detail = outputProduct.Details.FirstOrDefault(e => e.ProductId == outputProductCode.ProductId && e.InputId == outputProductCode.InputId);
                    if (detail == null) return 4;

                    detail.Quantity -= 1;
                    _context.WarehouseOutputProductDetails.Update(detail);

                    // xoa code
                    _context.WarehouseOutputProductCodes.Remove(outputProductCode);
                }

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
                var key = WebUtility.UrlDecode(search.Key.Trim());
                var productIds = await _context.WarehouseInputProductCodes.Where(e => e.ProductCode == key).Select(e => e.ProductId).ToListAsync();
                if (productIds != null && productIds.Count > 0) query = query.Where(e => productIds.Contains(e.ProductId));
                else query = query.Where(e => (e.Product.Code != null && e.Product.Code.Contains(key)) || e.Product.Item.ItemLanguages.Any(l => l.Title.Contains(key)));
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

        #region report
        public async Task<decimal> GetCOGS(Guid companyId)
        {
            var kyKeToan = await _context.WarehouseConfigs.Where(e => e.CompanyId == companyId && e.Key == "KyKeToan").Select(e => e.Value).FirstOrDefaultAsync();
            if (kyKeToan == null) return 0;
            else
            {
                var fromDate = DateTime.Now;
                switch (kyKeToan)
                {
                    case "1":
                        fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        break;
                    case "2":
                        var quy = DateTime.Now.Month / 3;
                        var mid = DateTime.Now.Month % 3;
                        if (mid == 0) quy = quy + 1;
                        var thangDauQuy = quy * 3 - 2;
                        fromDate = new DateTime(DateTime.Now.Year, thangDauQuy, 1);
                        break;
                    case "3":
                        fromDate = new DateTime(DateTime.Now.Year, 1, 1);
                        break;
                }
                var result = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate).Select(e => e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
                return result.Sum();
            }
        }
        public async Task<decimal> GetTotalCOGS(Guid companyId)
        {
            var result = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156)).Select(e => e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
            return result.Sum();
        }
        public async Task<decimal> GetCOGS(Guid companyId, DateTime fromDate, DateTime toDate)
        {
            var result = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
            return result.Sum();
        }
        public async Task<List<MoneyAccountingDto>> GetStageCOGS(Guid companyId)
        {
            var data = new List<MoneyAccountingDto>();

            var kyKeToan = await _context.WarehouseConfigs.Where(e => e.CompanyId == companyId && e.Key == "KyKeToan").Select(e => e.Value).FirstOrDefaultAsync();
            if (kyKeToan == null) return data;
            else
            {
                var company = await _context.Companies.FirstAsync(e => e.Id == companyId);
                var startDate = company.PublishDate == null ? company.CreateDate : company.PublishDate.Value;
                var fromYear = DateTime.Now.Year;
                switch (kyKeToan)
                {
                    case "1":
                        fromYear = startDate.Year;
                        while (fromYear <= DateTime.Now.Year)
                        {
                            var fromMonth = fromYear == startDate.Year ? startDate.Month : 1;
                            var toMonth = fromYear == DateTime.Now.Year ? DateTime.Now.Month: 12;

                            for (var i = fromMonth; i <= toMonth; i++)
                            {
                                var stageAmount = new MoneyAccountingDto
                                {
                                    Id = data.Count + 1,
                                    Title = $"Tháng {i} Năm {fromYear}",
                                    Date = $"{i}/{fromYear}"
                                };
                                var fromDate = new DateTime(fromYear, i, 1);
                                var toDate = fromDate.AddMonths(1).AddMilliseconds(-1);
                                var result = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
                                stageAmount.TotalAmount = result.Sum();
                                data.Add(stageAmount);
                            }
                            fromYear++;
                        }
                        break;
                    case "2":
                        fromYear = startDate.Year;
                        while (fromYear <= DateTime.Now.Year)
                        {
                            var fromMonth = fromYear == startDate.Year ? startDate.Month : 1;
                            var toMonth = fromYear == DateTime.Now.Year ? DateTime.Now.Month : 12;

                            for (var i = fromMonth; i <= toMonth; i += 3)
                            {
                                var stageAmount = new MoneyAccountingDto
                                {
                                    Id = data.Count + 1,
                                    Title = $"Quí {i / 3 + 1} Năm {fromYear}",
                                    Date = $"Quí {i / 3 + 1}/{fromYear}"
                                };
                                var fromDate = new DateTime(fromYear, i, 1);
                                var toDate = fromDate.AddMonths(3).AddMilliseconds(-1);
                                var result = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
                                stageAmount.TotalAmount = result.Sum();
                                data.Add(stageAmount);
                            }
                            fromYear++;
                        }
                        break;
                    case "3":
                        fromYear = startDate.Year;
                        while (fromYear <= DateTime.Now.Year)
                        {
                            var fromMonth = fromYear == startDate.Year ? startDate.Month : 1;
                            var toMonth = fromYear == DateTime.Now.Year ? DateTime.Now.Month : 12;

                            var stageAmount = new MoneyAccountingDto
                            {
                                Id = data.Count + 1,
                                Title = $"Năm {fromYear}",
                                Date = $"{fromYear}"
                            };
                            var fromDate = new DateTime(fromYear, fromMonth, 1);
                            var toDate = new DateTime(fromYear, toMonth, DateTime.DaysInMonth(fromYear, toMonth));
                            var result = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
                            stageAmount.TotalAmount = result.Sum();
                            data.Add(stageAmount);
                            fromYear++;
                        }
                        break;
                }

                return data;
            }
        }

        public async Task<decimal> GetExpensive(Guid companyId)
        {
            var kyKeToan = await _context.WarehouseConfigs.Where(e => e.CompanyId == companyId && e.Key == "KyKeToan").Select(e => e.Value).FirstOrDefaultAsync();
            if (kyKeToan == null) return 0;
            else
            {
                var fromDate = DateTime.Now;
                switch (kyKeToan)
                {
                    case "1":
                        fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        break;
                    case "2":
                        var quy = DateTime.Now.Month / 3;
                        var mid = DateTime.Now.Month % 3;
                        if (mid == 0) quy = quy + 1;
                        var thangDauQuy = quy * 3 - 2;
                        fromDate = new DateTime(DateTime.Now.Year, thangDauQuy, 1);
                        break;
                    case "3":
                        fromDate = new DateTime(DateTime.Now.Year, 1, 1);
                        break;
                }
                var COGS = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
                var deliveryFee = await _context.OrderDeliveries.Where(e => e.Order.CompanyId == companyId && e.Order.ReceiveDate != null && fromDate <= e.Order.ReceiveDate).SumAsync(e => e.DeliveryFee);
                var expensive = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Type == 153) && fromDate <= e.CreateDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();

                var result = COGS.Sum() + deliveryFee + expensive.Sum();
                return result;
            }
        }
        public async Task<decimal> GetTotalExpensive(Guid companyId)
        {
            var COGS = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156)).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
            var deliveryFee = await _context.OrderDeliveries.Where(e => e.Order.CompanyId == companyId && e.Order.ReceiveDate != null).SumAsync(e => e.DeliveryFee);
            var expensive = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Type == 153)).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
            var result = COGS.Sum() + deliveryFee + expensive.Sum();
            return result;
        }
        public async Task<decimal> GetExpensive(Guid companyId, DateTime fromDate, DateTime toDate)
        {
            var COGS = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
            var deliveryFee = await _context.OrderDeliveries.Where(e => e.Order.CompanyId == companyId && e.Order.ReceiveDate != null && fromDate <= e.Order.ReceiveDate && e.Order.ReceiveDate <= toDate).SumAsync(e => e.DeliveryFee);
            var expensive = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Type == 153) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
            var result = COGS.Sum() + deliveryFee + expensive.Sum();
            return result;
        }
        public async Task<List<MoneyAccountingDto>> GetStageExpensive(Guid companyId)
        {
            var data = new List<MoneyAccountingDto>();

            var kyKeToan = await _context.WarehouseConfigs.Where(e => e.CompanyId == companyId && e.Key == "KyKeToan").Select(e => e.Value).FirstOrDefaultAsync();
            if (kyKeToan == null) return data;
            else
            {
                var company = await _context.Companies.FirstAsync(e => e.Id == companyId);
                var startDate = company.PublishDate == null ? company.CreateDate : company.PublishDate.Value;
                var fromYear = DateTime.Now.Year;
                switch (kyKeToan)
                {
                    case "1":
                        fromYear = startDate.Year;
                        while (fromYear <= DateTime.Now.Year)
                        {
                            var fromMonth = fromYear == startDate.Year ? startDate.Month : 1;
                            var toMonth = fromYear == DateTime.Now.Year ? DateTime.Now.Month : 12;

                            for (var i = fromMonth; i <= toMonth; i++)
                            {
                                var stageAmount = new MoneyAccountingDto
                                {
                                    Id = data.Count + 1,
                                    Title = $"Tháng {i} Năm {fromYear}",
                                    Date = $"{i}/{fromYear}"
                                };
                                var fromDate = new DateTime(fromYear, i, 1);
                                var toDate = fromDate.AddMonths(1).AddMilliseconds(-1);

                                var COGS = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
                                var deliveryFee = await _context.OrderDeliveries.Where(e => e.Order.CompanyId == companyId && e.Order.ReceiveDate != null && fromDate <= e.Order.ReceiveDate && e.Order.ReceiveDate <= toDate).SumAsync(e => e.DeliveryFee);
                                var expensive = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Type == 153) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();

                                var result = COGS.Sum() + deliveryFee + expensive.Sum();
                                data.Add(stageAmount);
                            }
                            fromYear++;
                        }
                        break;
                    case "2":
                        fromYear = startDate.Year;
                        while (fromYear <= DateTime.Now.Year)
                        {
                            var fromMonth = fromYear == startDate.Year ? startDate.Month : 1;
                            var toMonth = fromYear == DateTime.Now.Year ? DateTime.Now.Month : 12;

                            for (var i = fromMonth; i <= toMonth; i += 3)
                            {
                                var stageAmount = new MoneyAccountingDto
                                {
                                    Id = data.Count + 1,
                                    Title = $"Quí {i / 3 + 1} Năm {fromYear}",
                                    Date = $"Quí {i / 3 + 1}/{fromYear}"
                                };
                                var fromDate = new DateTime(fromYear, i, 1);
                                var toDate = fromDate.AddMonths(3).AddMilliseconds(-1);

                                var COGS = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
                                var deliveryFee = await _context.OrderDeliveries.Where(e => e.Order.CompanyId == companyId && e.Order.ReceiveDate != null && fromDate <= e.Order.ReceiveDate && e.Order.ReceiveDate <= toDate).SumAsync(e => e.DeliveryFee);
                                var expensive = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Type == 153) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();

                                var result = COGS.Sum() + deliveryFee + expensive.Sum();
                                data.Add(stageAmount);
                            }
                            fromYear++;
                        }
                        break;
                    case "3":
                        fromYear = startDate.Year;
                        while (fromYear <= DateTime.Now.Year)
                        {
                            var fromMonth = fromYear == startDate.Year ? startDate.Month : 1;
                            var toMonth = fromYear == DateTime.Now.Year ? DateTime.Now.Month : 12;

                            var stageAmount = new MoneyAccountingDto
                            {
                                Id = data.Count + 1,
                                Title = $"Năm {fromYear}",
                                Date = $"{fromYear}"
                            };

                            var fromDate = new DateTime(fromYear, fromMonth, 1);
                            var toDate = new DateTime(fromYear, toMonth, DateTime.DaysInMonth(fromYear, toMonth));

                            var COGS = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Warehouse.Type == 152 || e.Warehouse.Type == 156) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();
                            var deliveryFee = await _context.OrderDeliveries.Where(e => e.Order.CompanyId == companyId && e.Order.ReceiveDate != null && fromDate <= e.Order.ReceiveDate && e.Order.ReceiveDate <= toDate).SumAsync(e => e.DeliveryFee);
                            var expensive = await _context.WarehouseInputs.Where(e => e.Warehouse.CompanyId == companyId && (e.Type == 153) && fromDate <= e.CreateDate && e.CreateDate <= toDate).Select(e => e.TotalPrice - e.Products.Sum(e => e.Price * e.Quantity)).ToListAsync();

                            var result = COGS.Sum() + deliveryFee + expensive.Sum();
                            data.Add(stageAmount);
                            fromYear++;
                        }
                        break;
                }

                return data;
            }
        }
        #endregion
    }
}
