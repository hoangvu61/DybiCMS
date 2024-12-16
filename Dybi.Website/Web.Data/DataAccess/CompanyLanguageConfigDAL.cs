namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class CompanyLanguageConfigDAL : RepositoryBase<WebEntities, CompanyLanguageConfig> , ICompanyLanguageConfigDAL
    {
        public CompanyLanguageConfigDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ICompanyLanguageConfigDAL : IRepositoryBase<CompanyLanguageConfig>
    {
    }
    
}
