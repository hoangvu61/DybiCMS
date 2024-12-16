namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ContactDAL : RepositoryBase<WebEntities, Contact> , IContactDAL
    {
        public ContactDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IContactDAL : IRepositoryBase<Contact>
    {
    }
    
}
