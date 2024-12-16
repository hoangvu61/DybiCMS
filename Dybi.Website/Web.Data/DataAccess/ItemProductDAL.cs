namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemProductDAL : RepositoryBase<WebEntities, ItemProduct> , IItemProductDAL
    {
        public ItemProductDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemProductDAL : IRepositoryBase<ItemProduct>
    {
    }
    
}
