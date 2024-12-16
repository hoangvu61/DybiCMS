namespace Web.Asp.Provider.Cache
{
    public interface IGlobalCachingProvider
    {
        void AddItem(string key, object value);
        object GetItem(string key);
    }
}