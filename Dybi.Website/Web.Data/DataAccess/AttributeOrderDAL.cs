namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AttributeOrderDAL : RepositoryBase<WebEntities, AttributeOrder> , IAttributeOrderDAL
    {
        public AttributeOrderDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAttributeOrderDAL : IRepositoryBase<AttributeOrder>
    {
    }
    
}
