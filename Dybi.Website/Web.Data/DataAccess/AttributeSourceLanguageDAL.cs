namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AttributeSourceLanguageDAL : RepositoryBase<WebEntities, AttributeSourceLanguage> , IAttributeSourceLanguageDAL
    {
        public AttributeSourceLanguageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAttributeSourceLanguageDAL : IRepositoryBase<AttributeSourceLanguage>
    {
    }
    
}
