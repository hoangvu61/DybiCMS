using Web.Models;
using Web.Models.SeedWork;

namespace Web.App.Services
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

        Task<PagedList<WarehouseInputDto>> GetWarehouseInputs(WarehouseIOSearch search);
        Task<WarehouseInputDto> GetWarehouseInput(string id);
        Task<string> CreateWarehouseInput(WarehouseInputRequest request);
        Task<string> DeleteInput(Guid id);

        Task<List<WarehouseProductInputDto>> GetWarehouseInputProducts(Guid id);
        Task<WarehouseProductInputDto> GetWarehouseInputProduct(Guid inputId, Guid productId);
        Task<string> CreateWarehouseInputProduct(Guid inputId, WarehouseProductInputRequest request);
        Task<string> DeleteWarehouseInputProduct(Guid inputid, Guid productid);

        Task<List<string>> GetWarehouseInputProductCodes(Guid inputid, Guid productid);
        Task<string> CreateWarehouseInputProductCode(Guid inputId, Guid productId, WarehouseProductCodeRequest request);
        Task<string> DeleteWarehouseInputProductCode(Guid inputid, Guid productid, string code);

        Task<PagedList<WarehouseOutputDto>> GetWarehouseOutputs(WarehouseIOSearch search);
        Task<WarehouseOutputDto> GetWarehouseOutput(string id);
        Task<string> CreateWarehouseOutput(WarehouseOutputRequest request);
        Task<string> DeleteOutput(Guid id);

        Task<List<WarehouseProductOutputDto>> GetWarehouseOutputProducts(Guid id);
        Task<string> CreateWarehouseOutputProduct(Guid outputId, WarehouseProductOutputRequest request);
        Task<string> CreateOutputProductByCode(Guid outputId, string code);
        Task<string> DeleteWarehouseOutputProduct(Guid outputId, Guid productid);
        Task<string> DeleteOutputProductByCode(Guid outputId, string code);

        Task<List<string>> GetWarehouseOutputProductCodes(Guid outputid, Guid productid);
        Task<string> CreateWarehouseOutputProductCode(Guid outputid, Guid productId, WarehouseProductCodeRequest request);
        Task<string> DeleteWarehouseOutputProductCode(Guid outputid, Guid productid, string code);

        Task<PagedList<WarehouseInventoryDto>> GetInventories(WarehouseInventorySearch search);
        Task<List<WarehouseInputInventoryDto>> GetInventories(Guid productid);
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
        Task<ProductDetailDto> GetProductById(string id);
        Task<ProductDetailDto> GetProductByCode(string code);
        Task<string> CreateProduct(WarehouseProductDto request);
        Task<bool> UpdateProduct(ProductDetailDto request);
        Task<bool> UpdateProductCategory(Guid itemId, Guid categoryId);
        Task<bool> DeleteProduct(Guid id);

        Task<List<ProductSeriDto>> SeriesByProduct(Guid productId);
        Task<List<ProductSeriDto>> ImageCodesByProduct(string productId);
        Task<bool> AddSearies(ProductSeriCreateRequest request);
        Task<bool> DeleteSearies(Guid productId, string series);
        #endregion

        #region report
        Task<ReportStatisticDto> GetStatisticCurrent();
        Task<ReportStatisticDto> GetStatisticTotal();

        Task<List<MoneyAccountingDto>> GetReportCostOfGrossSoldStage();
        Task<List<MoneyAccountingDto>> GetReportRevenueStage();
        Task<List<MoneyAccountingDto>> GetReportExpensiveStage();
        #endregion
    }
}
