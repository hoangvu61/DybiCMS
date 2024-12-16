namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AspNetUserTokenDAL : RepositoryBase<WebEntities, AspNetUserToken> , IAspNetUserTokenDAL
    {
        public AspNetUserTokenDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAspNetUserTokenDAL : IRepositoryBase<AspNetUserToken>
    {
    }
    
}
