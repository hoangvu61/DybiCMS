namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemProductAddOnDAL : RepositoryBase<WebEntities, ItemProductAddOn> , IItemProductAddOnDAL
    {
        public ItemProductAddOnDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemProductAddOnDAL : IRepositoryBase<ItemProductAddOn>
    {
    }
    
}
