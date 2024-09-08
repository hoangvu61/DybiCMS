using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Net;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models;
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
                .Include(e => e.Products)
                .Where(e => e.CompanyId == CompanyId && e.Id == OrderId);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> CreateOrder(Order order)
        {
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            _context.Orders.Update(order);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrder(Order order)
        {
            var products = await _context.OrderProducts.Where(e => e.OrderId == order.Id).ToArrayAsync();
            _context.OrderProducts.RemoveRange(products);

            var attributes = await _context.OrderAttributes.Where(e => e.OrderId == order.Id).ToArrayAsync();
            _context.OrderAttributes.RemoveRange(attributes);

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

        public async Task<int> ExportWarehouse(Order order)
        {
            var phuongPhapTinhGiaXuatKho = await _context.WarehouseConfigs.Where(e => e.CompanyId == order.CompanyId && e.Key == "PhuongPhapTinhGiaXuatKho").Select(e => e.Value).FirstOrDefaultAsync();
            if (phuongPhapTinhGiaXuatKho == null) return 1;
            if (phuongPhapTinhGiaXuatKho == "2") return 2;

            var warehouse = await _context.Warehouses.FirstOrDefaultAsync(e => e.Type == 156 && e.CompanyId == order.CompanyId);
            if (warehouse == null) return 3;
            var output = new WarehouseOutput
            {
                Id = Guid.NewGuid(),
                WarehouseId = warehouse.Id,
                Type = 1,
                CreateDate = DateTime.Now,
                ToOrder = new WarehouseOutputToOrder { OrderId = order.Id },
                Products = order.Products.Select(e => new WarehouseOutputProduct { ProductId = e.ProductId, Quantity = e.Quantity }).ToList(),
            };

            foreach (var product in output.Products)
            {
                var warehouseInventory = await _context.WarehouseInventories.FirstOrDefaultAsync(e => e.WarehouseId == warehouse.Id && e.ProductId == product.ProductId);
                if (warehouseInventory == null) return 4;
                if (warehouseInventory.InventoryNumber > product.Quantity) return 5;

                warehouseInventory.InventoryNumber -= product.Quantity;
                _context.WarehouseInventories.Update(warehouseInventory);

                var warehouseInputInventories = await _context.WarehouseInputInventories.Where(e => e.Input.WarehouseId == warehouse.Id && e.ProductId == product.ProductId).ToListAsync();
                if (warehouseInputInventories == null) return 6;
                if (warehouseInputInventories.Count == 0) return 7;
                
                var inputIds = warehouseInputInventories.Select(e => e.InputId).ToList();
                var inputProducts = await _context.WarehouseInputProducts.Where(e => inputIds.Contains(e.InputId)).OrderBy(e => e.Input.CreateDate).ToListAsync();

                foreach (var inputProduct in inputProducts)
                {
                    var detail = new WarehouseOutputProductDetail
                    {
                        InputId = inputProduct.InputId,
                        Price = inputProduct.Price,
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
            _context.WarehouseOutputs.Add(output);
            return 0;
        }
        public async Task<int> ImportWarehouse(Order order)
        {
            var warehouses = await _context.Warehouses.Where(e => e.CompanyId == order.Id).ToListAsync();
            if (warehouses == null || warehouses.Count == 0) return 1;
            var warehouse = warehouses.FirstOrDefault(e => e.Type == 156);
            if (warehouse == null) warehouse = warehouses.First();
            
            var input = new WarehouseInput
            {
                Id = Guid.NewGuid(),
                WarehouseId = warehouse.Id,
                Type = 1,
                CreateDate = DateTime.Now,
                FromOrder = new WarehouseInputFromOrder { OrderId = order.Id },
                Products = order.Products.Select(e => new WarehouseInputProduct { ProductId = e.ProductId, Quantity = e.Quantity }).ToList(),
            };

            foreach (var product in input.Products)
            {
                var warehouseInventory = await _context.WarehouseInventories.FirstOrDefaultAsync(e => e.WarehouseId == warehouse.Id && e.ProductId == product.ProductId);
                if (warehouseInventory == null)
                {
                    warehouseInventory = new WarehouseInventory { ProductId = product.ProductId, WarehouseId = input.WarehouseId, InventoryNumber = 0 };
                    _context.WarehouseInventories.Add(warehouseInventory);
                }
                else _context.WarehouseInventories.Update(warehouseInventory);

                warehouseInventory.InventoryNumber += product.Quantity;

                warehouseInventory = new WarehouseInventory
                {
                    WarehouseId = input.WarehouseId,
                    ProductId = product.ProductId,
                    InventoryNumber = product.Quantity,
                };
                _context.WarehouseInventories.Add(warehouseInventory);
            }
            _context.WarehouseInputs.Add(input);

            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<PagedList<Customer>> GetCustomers(Guid companyId, CustomerSearch search)
        {
            var query = _context.Customers.Include(e => e.Orders).ThenInclude(e => e.Products).Where(e => e.CompanyId == companyId);

            if (!string.IsNullOrEmpty(search.Key))
            {
                var key = search.Key.Trim();
                query = query.Where(e => (e.CustomerPhone != null && e.CustomerPhone.Contains(key)) 
                                            || (e.CustomerName != null && e.CustomerName.Contains(key))
                                            || (e.CustomerAddress != null && e.CustomerAddress.Contains(key)));
            }

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(e => e.CustomerName)
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();
            return new PagedList<Customer>(data, count, search.PageNumber, search.PageSize);
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
            var query = _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        #region
        public async Task<OrderDelivery> GetOrderDelivery(Guid orderId)
        {
            var result = await _context.OrderDeliveries.FirstOrDefaultAsync(e => e.OrderId == orderId);
            return result;
        }
        public async Task<bool> AddOrderDelivery(OrderDelivery delivery)
        {
            _context.OrderDeliveries.Add(delivery);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateOrderDelivery(OrderDelivery delivery)
        {
            _context.OrderDeliveries.Update(delivery);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderDelivery(OrderDelivery delivery)
        {
            _context.OrderDeliveries.Remove(delivery);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
