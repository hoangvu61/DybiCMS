namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AttributeContactDAL : RepositoryBase<WebEntities, AttributeContact> , IAttributeContactDAL
    {
        public AttributeContactDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAttributeContactDAL : IRepositoryBase<AttributeContact>
    {
    }
    
}
