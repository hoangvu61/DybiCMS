using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models.SeedWork;
using Web.Models;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore.Internal;
using System.Net;

namespace Web.Api.Repositories
{
    public class DebtRepository : IDebtRepository
    {
        private readonly WebDbContext _context;
        public DebtRepository(WebDbContext context)
        {
            _context = context;
        }
        public async Task<PagedList<DebtSupplier>> GetSupplierDebts(Guid companyId, DebtSupplierSearch search)
        {
            var query = _context.DebtSuppliers
                                .Include(e => e.Supplier)
                                .Where(e => e.Supplier.CompanyId == companyId);

            if (search.FromDate != null) query = query.Where(e => search.FromDate <= e.CreateDate);
            if (search.ToDate != null) query = query.Where(e => e.CreateDate <= search.ToDate);
            if (search.Type > 0) query = query.Where(e => e.Type == search.Type);
            if (search.DebtorOrCreditor_Id != null && search.DebtorOrCreditor_Id != Guid.Empty) query = query.Where(e => e.SupplierId == search.DebtorOrCreditor_Id);
            else
            {
                var lastestDepts = await query.GroupBy(r => r.SupplierId)
                                            .Select(g => new
                                            {
                                                SupplierId = g.Key,
                                                MaxDate = g.Max(e => e.CreateDate),
                                            }).ToListAsync();
                var supplierIds = lastestDepts.Select(e => e.SupplierId).ToArray();
                var dates = lastestDepts.Select(e => e.MaxDate).ToArray();
                query = query.Where(e => supplierIds.Contains(e.SupplierId) && dates.Contains(e.CreateDate));
            }

            var count = await query.CountAsync();

            var data = await query.OrderBy(e => e.CreateDate)
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();
            return new PagedList<DebtSupplier>(data, count, search.PageNumber, search.PageSize);
        }
        public async Task<DebtSupplier> GetSupplierDebt(Guid companyId, Guid id)
        {
            var query = _context.DebtSuppliers
                        .Include(e => e.Supplier)
                        .Where(e => e.Supplier.CompanyId == companyId && e.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<int> CreateSupplierDebt(DebtSupplier debt)
        {
            var lastestDebt = await _context.DebtSuppliers.Where(e => e.SupplierId == debt.SupplierId)
                   .OrderByDescending(e => e.CreateDate)
                   .FirstOrDefaultAsync();
            if (lastestDebt == null) return 1;
            debt.TotalDebt = lastestDebt.TotalDebt - debt.Price;

            _context.DebtSuppliers.Add(debt);
            await _context.SaveChangesAsync();
            return 0;
        }
        public async Task<int> DeleteSupplierDebt(DebtSupplier debt)
        {
            // chỉ dòng dữ liệu Trả Nợ Cuối Cùng mới được xóa
            // xóa ở giữa lủng dữ liệu cột TotalPrice
            var lastestDebt = await _context.DebtSuppliers.Where(e => e.SupplierId == debt.SupplierId)
                   .OrderByDescending(e => e.CreateDate)
                   .FirstOrDefaultAsync();
            if (lastestDebt == null) return 1;
            if (lastestDebt.Type == 1) return 2;
            if (lastestDebt.Id != debt.Id) return 3; 
            _context.DebtSuppliers.Remove(debt);
            await _context.SaveChangesAsync();
            return 0;
        }


        public async Task<PagedList<DebtCustomer>> GetCustomerDebts(Guid companyId, DebtCustomerSearch search)
        {
            var query = _context.DebtCustomers
                                .Include(e => e.Customer)
                                .Where(e => e.Customer.CompanyId == companyId);

            if (search.FromDate != null) query = query.Where(e => search.FromDate <= e.CreateDate);
            if (search.ToDate != null) query = query.Where(e => e.CreateDate <= search.ToDate);
            if (search.Type > 0) query = query.Where(e => e.Type == search.Type);
            if (!string.IsNullOrEmpty(search.Key))
            {
                var key = WebUtility.UrlDecode(search.Key);
                query = query.Where(e => e.Customer.CustomerName.Contains(key) || e.Customer.CustomerPhone.Contains(key));
            }
            else
            {
                var lastestDepts = await query.GroupBy(r => r.CustomerId)
                                           .Select(g => new
                                           {
                                               SupplierId = g.Key,
                                               MaxDate = g.Max(e => e.CreateDate),
                                           }).ToListAsync();
                var customerIds = lastestDepts.Select(e => e.SupplierId).ToArray();
                var dates = lastestDepts.Select(e => e.MaxDate).ToArray();
                query = query.Where(e => customerIds.Contains(e.CustomerId) && dates.Contains(e.CreateDate));
            }    

            var count = await query.CountAsync();

            var data = await query.OrderBy(e => e.CreateDate)
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();
            return new PagedList<DebtCustomer>(data, count, search.PageNumber, search.PageSize);
        }
        public async Task<DebtCustomer> GetCustomerDebt(Guid companyId, Guid id)
        {
            var query = _context.DebtCustomers
                        .Include(e => e.Customer)
                        .Where(e => e.Customer.CompanyId == companyId && e.Id == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<int> CreateCustomerDebt(DebtCustomer debt)
        {
            var lastestDebt = await _context.DebtCustomers.Where(e => e.CustomerId == debt.CustomerId)
                  .OrderByDescending(e => e.CreateDate)
                  .FirstOrDefaultAsync();
            if (lastestDebt == null) return 1;
            debt.TotalDebt = lastestDebt.TotalDebt - debt.Debt;

            _context.DebtCustomers.Add(debt);
            await _context.SaveChangesAsync();
            return 0;
        }
        public async Task<int> DeleteCustomerDebt(DebtCustomer debt)
        {
            // chỉ dòng dữ liệu Trả Nợ Cuối Cùng mới được xóa
            // xóa ở giữa lủng dữ liệu cột TotalPrice
            var lastestDebt = await _context.DebtCustomers.Where(e => e.CustomerId == debt.CustomerId && e.Type == 2)
                   .OrderByDescending(e => e.CreateDate)
                   .FirstOrDefaultAsync();
            if (lastestDebt != null && lastestDebt.Id == debt.Id) return 1;
            _context.DebtCustomers.Remove(debt);
            await _context.SaveChangesAsync();
            return 0;
        }
    }
}
