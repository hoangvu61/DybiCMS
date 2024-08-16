using Web.Api.Entities;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public interface IWarehouseRepository
    {
        #region config
        Task<List<Config>> GetConfigDefaults();
        Task<List<WarehouseConfig>> GetConfigs(Guid companyId);
        Task<string?> GetConfig(Guid companyId, string key);
        Task<WarehouseConfig> SetConfig(Guid companyId, string key, string? value);
        #endregion

        #region warehouse
        Task<List<Warehouse>> GetWarehouses(Guid companyId);
        Task<Warehouse?> GetWarehouse(Guid id);
        Task<Warehouse> CreateWarehouse(Warehouse warehouse);
        Task<Warehouse> UpdateWarehouse(Warehouse warehouse);
        Task<Warehouse> DeleteWarehouse(Warehouse warehouse);


        Task<PagedList<WarehouseInput>> GetInputs(Guid companyId, WarehouseInputSearch search);
        Task<WarehouseInput?> GetInput(Guid companyId, Guid inputId);
        Task<bool> CheckExistInputCode(Guid companyId, string inputCode);
        Task<bool> CheckExistProducts(Guid companyId, Guid inputId);
        Task<WarehouseInput> CreateInput(WarehouseInput input);
        Task<WarehouseInput> DeleteInput(WarehouseInput warehouseInput);


        Task<List<WarehouseInputProduct>> GetInputProducts(Guid companyId, Guid inputId);
        Task<WarehouseInputProduct> GetInputProduct(Guid companyId, Guid inputId, Guid productId);
        Task<bool> CheckExistProductInput(Guid companyId, Guid inputId, Guid productId);
        Task<WarehouseInputProduct> CreateInputProduct(WarehouseInputProduct product);
        Task<int> DeleteInputProduct(WarehouseInputProduct product);

        Task<List<WarehouseInputProductCode>> GetInputProductCodes(Guid companyId, Guid inputId, Guid productId);
        Task<WarehouseInputProductCode> GetInputProductCode(Guid companyId, Guid inputId, Guid productId, string code);
        Task<bool> CheckExistProductInputCode(Guid companyId, Guid productId, string code);
        Task<int> CreateInputProductCode(WarehouseInputProductCode productCode);
        Task<int> DeleteInputProductCode(WarehouseInputProductCode productCode);
        #endregion

        #region factory
        Task<List<WarehouseFactory>> GetFactories(Guid companyId);
        Task<WarehouseFactory?> GetFactory(Guid id);
        Task<WarehouseFactory> CreateFactory(WarehouseFactory factory);
        Task<WarehouseFactory> UpdateFactory(WarehouseFactory factory);
        Task<WarehouseFactory> DeleteFactory(WarehouseFactory factory);
        #endregion

        #region supplier
        Task<List<WarehouseSupplier>> GetSuppliers(Guid companyId);
        Task<WarehouseSupplier?> GetSupplier(Guid id);
        Task<WarehouseSupplier> CreateSupplier(WarehouseSupplier supplier);
        Task<WarehouseSupplier> UpdateSupplier(WarehouseSupplier supplier);
        Task<WarehouseSupplier> DeleteSupplier(WarehouseSupplier factory);
        #endregion

        #region inventory
        Task<PagedList<WarehouseInventory>> GetInventories(Guid companyId, WarehouseInventorySearch search);
        Task<List<WarehouseInputInventory>> GetInventories(Guid companyId, Guid productId);
        #endregion
    }
}
