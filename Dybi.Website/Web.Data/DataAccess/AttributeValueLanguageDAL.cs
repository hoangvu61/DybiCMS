namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class AttributeValueLanguageDAL : RepositoryBase<WebEntities, AttributeValueLanguage> , IAttributeValueLanguageDAL
    {
        public AttributeValueLanguageDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IAttributeValueLanguageDAL : IRepositoryBase<AttributeValueLanguage>
    {
    }
    
}
