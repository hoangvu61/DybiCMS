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


        Task<PagedList<WarehouseInput>> GetInputs(Guid companyId, WarehouseIOSearch search);
        Task<WarehouseInput?> GetInput(Guid companyId, Guid inputId);
        Task<bool> CheckExistInputCode(Guid companyId, string inputCode);
        Task<bool> CheckExistProductInInput(Guid companyId, Guid inputId, Guid productId);
        Task<int> CreateInput(WarehouseInput input);
        Task<WarehouseInput> DeleteInput(WarehouseInput warehouseInput);


        Task<List<WarehouseInputProduct>> GetInputProducts(Guid companyId, Guid inputId);
        Task<WarehouseInputProduct> GetInputProduct(Guid companyId, Guid inputId, Guid productId);
        Task<int> CreateInputProduct(WarehouseInputProduct product);
        Task<int> DeleteInputProduct(WarehouseInputProduct product);

        Task<List<WarehouseInputProductCode>> GetInputProductCodes(Guid companyId, Guid inputId, Guid productId);
        Task<WarehouseInputProductCode> GetInputProductCode(Guid companyId, Guid inputId, Guid productId, string code);
        Task<bool> CheckExistProductInputCode(Guid companyId, Guid productId, string code);
        Task<int> CreateInputProductCode(WarehouseInputProductCode productCode);
        Task<int> DeleteInputProductCode(WarehouseInputProductCode productCode);


        Task<PagedList<WarehouseOutput>> GetOutputs(Guid companyId, WarehouseIOSearch search);
        Task<WarehouseOutput?> GetOutput(Guid companyId, Guid inputId);
        Task<Order> GetOutputOrder(Guid companyId, Guid orderId);
        Task<bool> CheckExistProductInOutputs(Guid companyId, Guid outputId, Guid productId);
        Task<int> CreateOutput(Guid companyId, WarehouseOutput output);
        Task<WarehouseOutput> DeleteOutput(WarehouseOutput warehouseOutput);

        Task<List<WarehouseOutputProductDetail>> GetOutputProducts(Guid companyId, Guid outputId);
        Task<WarehouseOutputProduct> GetOutputProduct(Guid companyId, Guid outputId, Guid productId);
        Task<int> CreateOutputProduct(Guid companyId, WarehouseOutputProduct product);
        Task<int> DeleteOutputProduct(WarehouseOutputProduct product);

        Task<List<WarehouseOutputProductCode>> GetOutputProductCodes(Guid companyId, Guid outputId, Guid productId);
        Task<WarehouseOutputProductCode> GetOutputProductCode(Guid companyId, Guid outputId, Guid productId, string code);
        Task<bool> CheckExistProductOutputCode(Guid companyId, Guid productId, string code);
        Task<int> CreateOutputProductCode(Guid companyId, Guid outputId, Guid productId, string productCode);
        Task<int> CreateOutputProductCode(Guid companyId, Guid outputId, string productCode);
        Task<int> DeleteOutputProductCode(Guid companyId, Guid outputId, string productCode);
        Task<int> DeleteOutputProductCode(Guid companyId, Guid outputId, Guid productId, string productCode);
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
