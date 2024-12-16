namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AspNetUserClaimDAL : RepositoryBase<WebEntities, AspNetUserClaim> , IAspNetUserClaimDAL
    {
        public AspNetUserClaimDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAspNetUserClaimDAL : IRepositoryBase<AspNetUserClaim>
    {
    }
    
}
