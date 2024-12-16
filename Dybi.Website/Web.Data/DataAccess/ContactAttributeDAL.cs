namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ContactAttributeDAL : RepositoryBase<WebEntities, ContactAttribute> , IContactAttributeDAL
    {
        public ContactAttributeDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IContactAttributeDAL : IRepositoryBase<ContactAttribute>
    {
    }
    
}
