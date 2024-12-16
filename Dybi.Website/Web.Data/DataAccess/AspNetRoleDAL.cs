namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AspNetRoleDAL : RepositoryBase<WebEntities, AspNetRole> , IAspNetRoleDAL
    {
        public AspNetRoleDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAspNetRoleDAL : IRepositoryBase<AspNetRole>
    {
    }
    
}
