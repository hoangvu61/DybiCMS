namespace Web.Data.DataAccess
{
	using  Web.Data.Infrastructure;

    public partial class ModuleConfigDetailDAL : RepositoryBase<WebEntities, ModuleConfigDetail> , IModuleConfigDetailDAL
    {
        public ModuleConfigDetailDAL(IDatabaseFactory<WebEntities> databaseFactory) : base(databaseFactory)
    	{
    	}
    }
    
    public partial interface IModuleConfigDetailDAL : IRepositoryBase<ModuleConfigDetail>
    {
    }
    
}
