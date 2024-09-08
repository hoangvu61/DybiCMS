using Web.Models;
using Web.Models.SeedWork;

namespace Dybi.App.Services
{
    public interface IOrderApiClient
    {
        #region order
        Task<PagedList<OrderDto>> GetNewOrders(PagingParameters paging);
        Task<PagedList<OrderDto>> GetConfirmOrders(PagingParameters paging);
        Task<PagedList<OrderDto>> GetSendOrders(PagingParameters paging);
        Task<PagedList<OrderDto>> GetReceiveOrders(PagingParameters paging);
        Task<PagedList<OrderDto>> GetCancelOrders(PagingParameters paging);
        Task<OrderDetailDto> GetOrder(Guid id);
        Task<bool> ConfirmOrder(Guid id);
        Task<bool> SendOrder(Guid id);
        Task<bool> ReceiveOrder(Guid id);
        Task<bool> CancelOrder(Guid id);
        Task<bool> DeleteOrder(Guid id);

        Task<string> CreateOrder(OrderRequest request);
        #endregion

        #region product
        Task<List<OrderProductDto>> GetOrderProducts(Guid id);
        Task<bool> CreateOrderProduct(Guid id, OrderProductDto dto);
        Task<bool> UpdateOrderProduct(Guid id, OrderProductDto dto);
        Task<bool> DeleteOrderProduct(Guid id, Guid productId);
        #endregion

        #region customer
        Task<PagedList<CustomerDto>> GetCusomers(CustomerSearch search);
        Task<bool> UpdateCustomer(OrderCustomerDto request);
        #endregion

        #region deliver
        Task<OrderDeliveryDto> GetOrderDelivery(Guid id);

        Task<bool> UpdateOrderDelivery(Guid id, OrderDeliveryRequest request);

        Task<bool> DeleteOrderDelivery(Guid id);
        #endregion

        #region settings

        #endregion
    }
}
