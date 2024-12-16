namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemArticleDAL : RepositoryBase<WebEntities, ItemArticle> , IItemArticleDAL
    {
        public ItemArticleDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemArticleDAL : IRepositoryBase<ItemArticle>
    {
    }
    
}
