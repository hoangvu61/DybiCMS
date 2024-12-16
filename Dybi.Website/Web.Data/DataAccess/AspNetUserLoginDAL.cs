namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AspNetUserLoginDAL : RepositoryBase<WebEntities, AspNetUserLogin> , IAspNetUserLoginDAL
    {
        public AspNetUserLoginDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAspNetUserLoginDAL : IRepositoryBase<AspNetUserLogin>
    {
    }
    
}
