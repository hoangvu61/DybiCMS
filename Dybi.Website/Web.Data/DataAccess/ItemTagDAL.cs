namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemTagDAL : RepositoryBase<WebEntities, ItemTag> , IItemTagDAL
    {
        public ItemTagDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemTagDAL : IRepositoryBase<ItemTag>
    {
    }
    
}
