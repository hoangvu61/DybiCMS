namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class WebInfoDAL : RepositoryBase<WebEntities, WebInfo> , IWebInfoDAL
    {
        public WebInfoDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IWebInfoDAL : IRepositoryBase<WebInfo>
    {
    }
    
}
