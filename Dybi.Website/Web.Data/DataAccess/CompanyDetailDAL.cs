namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CompanyDetailDAL : RepositoryBase<WebEntities, CompanyDetail> , ICompanyDetailDAL
    {
        public CompanyDetailDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICompanyDetailDAL : IRepositoryBase<CompanyDetail>
    {
    }
    
}
