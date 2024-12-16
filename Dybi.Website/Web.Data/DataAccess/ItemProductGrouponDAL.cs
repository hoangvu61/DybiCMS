namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemProductGrouponDAL : RepositoryBase<WebEntities, ItemProductGroupon> , IItemProductGrouponDAL
    {
        public ItemProductGrouponDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemProductGrouponDAL : IRepositoryBase<ItemProductGroupon>
    {
    }
    
}
