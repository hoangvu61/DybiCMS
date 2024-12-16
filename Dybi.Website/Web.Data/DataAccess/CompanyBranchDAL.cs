namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CompanyBranchDAL : RepositoryBase<WebEntities, CompanyBranch> , ICompanyBranchDAL
    {
        public CompanyBranchDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICompanyBranchDAL : IRepositoryBase<CompanyBranch>
    {
    }
    
}
