namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AttributeSourceDAL : RepositoryBase<WebEntities, AttributeSource> , IAttributeSourceDAL
    {
        public AttributeSourceDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAttributeSourceDAL : IRepositoryBase<AttributeSource>
    {
    }
    
}
