using Web.Models;
using Web.Models.SeedWork;

namespace Dybi.App.Services
{
    public interface IWarehouseApiClient
    {
        Task<List<ConfigDefaultDto>> GetConfigDefaults();
        Task<List<ConfigDto>> GetConfigs();
        Task<string> GetConfig(string key);
        Task<bool> SetConfig(string key, string value);


        #region Warehouse
        Task<List<WarehouseDto>> GetWarehouses();
        Task<WarehouseDto> GetWarehouse(Guid id);
        Task<bool> CreateWarehouse(WarehouseDto request);
        Task<bool> UpdateWarehouse(WarehouseDto request);
        Task<bool> DeleteWarehouse(Guid id);

        Task<PagedList<WarehouseInputDto>> GetWarehouseInputs(WarehouseInputSearch search);
        #endregion

        #region Xuong san xuat
        Task<List<WarehouseFactoryDto>> GetFactories();
        Task<WarehouseFactoryDto> GetFactory(Guid id);
        Task<bool> CreateFactory(WarehouseFactoryDto request);
        Task<bool> UpdateFactory(WarehouseFactoryDto request);
        Task<bool> DeleteFactory(Guid id);
        #endregion

        #region Nha cung cap
        Task<List<WarehouseSupplierDto>> GetSuppliers();
        Task<WarehouseSupplierDto> GetSupplier(Guid id);
        Task<bool> CreateSupplier(WarehouseSupplierDto request);
        Task<bool> UpdateSupplier(WarehouseSupplierDto request);
        Task<bool> DeleteSupplier(Guid id);
        #endregion

        #region Danh muc san pham
        Task<List<WarehouseCategoryDto>> GetCategories();
        Task<WarehouseCategoryDto> GetCategory(Guid id);
        Task<bool> CreateCategory(WarehouseCategoryDto request);
        Task<bool> UpdateCategory(WarehouseCategoryDto request);
        Task<bool> PublishCategory(WarehouseCategoryDto request);
        Task<bool> DeleteCategory(Guid id);
        #endregion

        #region San pham
        Task<PagedList<WarehouseProductDto>> GetProducts(ProductListSearch paging);
        Task<ProductDetailDto> GetProduct(string id);
        Task<bool> CreateProduct(WarehouseProductDto request);
        Task<bool> UpdateProduct(ProductDetailDto request);
        Task<bool> UpdateProductCategory(Guid itemId, Guid categoryId);
        Task<bool> DeleteProduct(Guid id);
        #endregion
    }
}
