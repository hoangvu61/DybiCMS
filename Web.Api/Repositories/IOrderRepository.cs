using Web.Api.Entities;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public interface IOrderRepository
    {
        Task<PagedList<Order>> GetNewOrders(Guid companyId, PagingParameters paging);
        Task<PagedList<Order>> GetConfirmOrders(Guid companyId, PagingParameters paging);
        Task<PagedList<Order>> GetSendOrders(Guid companyId, PagingParameters paging);
        Task<PagedList<Order>> GetReceiveOrders(Guid companyId, PagingParameters paging);
        Task<PagedList<Order>> GetCencelOrders(Guid companyId, PagingParameters paging);
        Task<Order> GetOrder(Guid companyId, Guid orderId);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(Order order);

        Task<List<OrderProduct>> GetOrderProducts(Guid companyId, Guid orderId);
        Task<OrderProduct> GetOrderProduct(Guid companyId, Guid orderId, Guid productId);
        Task<bool> AddOrderProduct(OrderProduct order);
        Task<bool> UpdateOrderProduct(OrderProduct product);
        Task<bool> DeleteOrderProduct(OrderProduct order);
        Task<int> ExportWarehouse(Order order);
        Task<int> ImportWarehouse(Order order);

        Task<Customer> GetCustomer(Guid companyId, Guid customerId);
        Task<bool> UpdateCustomer(Customer customer);

        Task<OrderDelivery> GetOrderDelivery(Guid orderId);
        Task<bool> AddOrderDelivery(OrderDelivery delivery);
        Task<bool> UpdateOrderDelivery(OrderDelivery delivery);
        Task<bool> DeleteOrderDelivery(OrderDelivery delivery);
    }
}
