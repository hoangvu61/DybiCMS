using Web.Models;
using Web.Models.SeedWork;

namespace Dybi.App.Services
{
    public interface IDebtApiClient
    {
        Task<PagedList<DebtDto>> GetSupplierDebts(DebtSupplierSearch search);
        Task<DebtDto> GetSupplierDebt(Guid id);
        Task<string> CreateSupplierDebt(DebtRequest request);
        Task<string> DeleteSupplierDebt(Guid id);

        Task<PagedList<DebtDto>> GetCustomerDebts(DebtCustomerSearch search);
        Task<DebtDto> GetCustomerDebt(Guid id);
        Task<string> CreateCustomerDebt(DebtRequest request);
        Task<string> DeleteCustomerDebt(Guid id);
    }
}
