namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AspNetRoleClaimDAL : RepositoryBase<WebEntities, AspNetRoleClaim> , IAspNetRoleClaimDAL
    {
        public AspNetRoleClaimDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAspNetRoleClaimDAL : IRepositoryBase<AspNetRoleClaim>
    {
    }
    
}
