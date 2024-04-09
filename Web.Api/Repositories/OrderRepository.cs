using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebDbContext _context;
        public OrderRepository(WebDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Order>> GetNewOrders(Guid CompanyId, PagingParameters paging)
        {
            var query = _context.Orders
                .Include(e => e.Customer)
                .Include(e => e.Products)
                .Where(e => e.CompanyId == CompanyId && e.ConfirmDate == null);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(e => e.CreateDate)
               .Skip((paging.PageNumber - 1) * paging.PageSize)
               .Take(paging.PageSize)
               .ToListAsync();
            return new PagedList<Order>(data, count, paging.PageNumber, paging.PageSize);
        }

        public async Task<PagedList<Order>> GetConfirmOrders(Guid CompanyId, PagingParameters paging)
        {
            var query = _context.Orders
                .Include(e => e.Customer)
                .Include(e => e.Products)
                .Where(e => e.CompanyId == CompanyId && e.ConfirmDate != null && e.SendDate == null);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(e => e.ConfirmDate)
               .Skip((paging.PageNumber - 1) * paging.PageSize)
               .Take(paging.PageSize)
               .ToListAsync();
            return new PagedList<Order>(data, count, paging.PageNumber, paging.PageSize);
        }

        public async Task<PagedList<Order>> GetSendOrders(Guid CompanyId, PagingParameters paging)
        {
            var query = _context.Orders
                .Include(e => e.Customer)
                .Include(e => e.Products)
                .Where(e => e.CompanyId == CompanyId && e.SendDate != null && (e.ReceiveDate == null && e.CancelDate == null));

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(e => e.SendDate)
               .Skip((paging.PageNumber - 1) * paging.PageSize)
               .Take(paging.PageSize)
               .ToListAsync();
            return new PagedList<Order>(data, count, paging.PageNumber, paging.PageSize);
        }

        public async Task<PagedList<Order>> GetReceiveOrders(Guid CompanyId, PagingParameters paging)
        {
            var query = _context.Orders
                .Include(e => e.Customer)
                .Include(e => e.Products)
                .Where(e => e.CompanyId == CompanyId && e.ReceiveDate != null);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(e => e.ReceiveDate)
               .Skip((paging.PageNumber - 1) * paging.PageSize)
               .Take(paging.PageSize)
               .ToListAsync();
            return new PagedList<Order>(data, count, paging.PageNumber, paging.PageSize);
        }

        public async Task<PagedList<Order>> GetCencelOrders(Guid CompanyId, PagingParameters paging)
        {
            var query = _context.Orders
                .Include(e => e.Customer)
                .Include(e => e.Products)
                .Where(e => e.CompanyId == CompanyId && e.CancelDate != null);

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(e => e.CancelDate)
               .Skip((paging.PageNumber - 1) * paging.PageSize)
               .Take(paging.PageSize)
               .ToListAsync();
            return new PagedList<Order>(data, count, paging.PageNumber, paging.PageSize);
        }

        public async Task<Order> GetOrder(Guid CompanyId, Guid OrderId)
        {
            var query = _context.Orders
                .Include(e => e.Customer)
                .Where(e => e.CompanyId == CompanyId && e.Id == OrderId);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            var query = _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrder(Order order)
        {
            var products = await _context.OrderProducts.Where(e => e.OrderId == order.Id).ToArrayAsync();
            _context.OrderProducts.RemoveRange(products);

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderProduct>> GetOrderProducts(Guid companyId, Guid orderId)
        {
            var query = _context.OrderProducts
                .Include(e => e.Product)
                .Include(e => e.Product.Item)
                .Include(e => e.Product.Item.ItemLanguages)
                .Where(e => e.Order.CompanyId == companyId && e.OrderId == orderId);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<OrderProduct> GetOrderProduct(Guid companyId, Guid orderId, Guid productId)
        {
            var query = _context.OrderProducts
                .Where(e => e.Order.CompanyId == companyId && e.OrderId == orderId && e.ProductId == productId);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> AddOrderProduct(OrderProduct product)
        {
            var query = _context.OrderProducts.Add(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateOrderProduct(OrderProduct product)
        {
            var query = _context.OrderProducts.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteOrderProduct(OrderProduct order)
        {
            _context.OrderProducts.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Customer> GetCustomer(Guid companyId, Guid customerId)
        {
            var query = _context.Customers
                .Where(e => e.CompanyId == companyId && e.Id == customerId);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var query = _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
