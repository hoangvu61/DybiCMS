namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemRelatedDAL : RepositoryBase<WebEntities, ItemRelated> , IItemRelatedDAL
    {
        public ItemRelatedDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemRelatedDAL : IRepositoryBase<ItemRelated>
    {
    }
    
}
