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
        Task<bool> CheckExistCode(Guid companyId, string inputCode);
        Task<bool> CheckExistProducts(Guid companyId, Guid inputId);
        Task<WarehouseInput> CreateInput(WarehouseInput input);
        Task<WarehouseInput> DeleteInput(WarehouseInput warehouseInput);
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
    }
}
