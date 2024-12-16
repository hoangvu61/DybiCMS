namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class TemplateLanguageKeyDAL : RepositoryBase<WebEntities, TemplateLanguageKey> , ITemplateLanguageKeyDAL
    {
        public TemplateLanguageKeyDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface ITemplateLanguageKeyDAL : IRepositoryBase<TemplateLanguageKey>
    {
    }
    
}
