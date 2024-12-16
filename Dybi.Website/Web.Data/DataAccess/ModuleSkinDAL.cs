namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ModuleSkinDAL : RepositoryBase<WebEntities, ModuleSkin> , IModuleSkinDAL
    {
        public ModuleSkinDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IModuleSkinDAL : IRepositoryBase<ModuleSkin>
    {
    }
    
}
