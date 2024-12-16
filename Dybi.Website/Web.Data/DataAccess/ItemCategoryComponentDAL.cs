namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemCategoryComponentDAL : RepositoryBase<WebEntities, ItemCategoryComponent> , IItemCategoryComponentDAL
    {
        public ItemCategoryComponentDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemCategoryComponentDAL : IRepositoryBase<ItemCategoryComponent>
    {
    }
    
}
