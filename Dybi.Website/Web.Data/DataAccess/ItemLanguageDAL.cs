namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemLanguageDAL : RepositoryBase<WebEntities, ItemLanguage> , IItemLanguageDAL
    {
        public ItemLanguageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemLanguageDAL : IRepositoryBase<ItemLanguage>
    {
    }
    
}
