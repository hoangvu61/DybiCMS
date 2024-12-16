namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class OrderDeliveryDAL : RepositoryBase<WebEntities, OrderDelivery> , IOrderDeliveryDAL
    {
        public OrderDeliveryDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IOrderDeliveryDAL : IRepositoryBase<OrderDelivery>
    {
    }
    
}
