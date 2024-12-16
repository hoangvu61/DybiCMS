namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ItemReviewDAL : RepositoryBase<WebEntities, ItemReview> , IItemReviewDAL
    {
        public ItemReviewDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IItemReviewDAL : IRepositoryBase<ItemReview>
    {
    }
    
}
