namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemMediaDAL : RepositoryBase<WebEntities, ItemMedia> , IItemMediaDAL
    {
        public ItemMediaDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemMediaDAL : IRepositoryBase<ItemMedia>
    {
    }
    
}
