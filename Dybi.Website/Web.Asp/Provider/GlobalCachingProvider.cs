namespace Web.Asp.Provider
{
    using Web.Asp.Provider.Cache;
    using System;

    public class GlobalCachingProvider : CachingProviderBase, IGlobalCachingProvider
    {
        #region Singleton 

        protected GlobalCachingProvider()
        {
        }

        public static GlobalCachingProvider Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }
            internal static readonly GlobalCachingProvider instance = new GlobalCachingProvider();
        }

        #endregion

        #region ICachingProvider

        public virtual new void AddItem(string key, object value)
        {
            base.AddItem(key, value);
        }

        public T GetItem<T>(string key)
        {
            var res = base.GetItem(key, false);
            if (res != null)
            {
                return res is T ? (T)res : default(T);
            }
            else return default(T);
        }

        public virtual object GetItem(string key)
        {
            return base.GetItem(key, false);
        }

        #endregion
    }
}