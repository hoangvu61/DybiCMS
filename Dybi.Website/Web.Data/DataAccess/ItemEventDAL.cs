namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemEventDAL : RepositoryBase<WebEntities, ItemEvent> , IItemEventDAL
    {
        public ItemEventDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemEventDAL : IRepositoryBase<ItemEvent>
    {
    }
    
}
