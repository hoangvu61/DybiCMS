namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AttributeLanguageDAL : RepositoryBase<WebEntities, AttributeLanguage> , IAttributeLanguageDAL
    {
        public AttributeLanguageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAttributeLanguageDAL : IRepositoryBase<AttributeLanguage>
    {
    }
    
}
