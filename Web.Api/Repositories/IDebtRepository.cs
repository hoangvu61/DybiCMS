using Web.Api.Entities;
using Web.Models.SeedWork;
using Web.Models;

namespace Web.Api.Repositories
{
    public interface IDebtRepository
    {
        Task<PagedList<DebtSupplier>> GetSupplierDebts(Guid companyId, DebtSupplierSearch search);
        Task<DebtSupplier> GetSupplierDebt(Guid companyId, Guid id);
        Task<int> CreateSupplierDebt(DebtSupplier debt);
        Task<int> DeleteSupplierDebt(DebtSupplier debt);

        Task<PagedList<DebtCustomer>> GetCustomerDebts(Guid companyId, DebtCustomerSearch search);
        Task<DebtCustomer> GetCustomerDebt(Guid companyId, Guid id);
        Task<int> CreateCustomerDebt(DebtCustomer debt);
        Task<int> DeleteCustomerDebt(DebtCustomer debt);

        Task<decimal> GetDebtAccountsReceivable(Guid companyId);
        Task<decimal> GetDebtAccountsPayvable(Guid companyId);
    }
}
