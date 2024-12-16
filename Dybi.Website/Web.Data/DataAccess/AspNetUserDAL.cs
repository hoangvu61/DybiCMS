namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AspNetUserDAL : RepositoryBase<WebEntities, AspNetUser> , IAspNetUserDAL
    {
        public AspNetUserDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAspNetUserDAL : IRepositoryBase<AspNetUser>
    {
    }
    
}
