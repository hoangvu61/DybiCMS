namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemCategoryDAL : RepositoryBase<WebEntities, ItemCategory> , IItemCategoryDAL
    {
        public ItemCategoryDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemCategoryDAL : IRepositoryBase<ItemCategory>
    {
    }
    
}
