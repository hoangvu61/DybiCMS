namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemAttributeDAL : RepositoryBase<WebEntities, ItemAttribute> , IItemAttributeDAL
    {
        public ItemAttributeDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemAttributeDAL : IRepositoryBase<ItemAttribute>
    {
    }
    
}
