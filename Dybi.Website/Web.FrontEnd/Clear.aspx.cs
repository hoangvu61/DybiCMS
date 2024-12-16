namespace Web.FrontEnd.Components.Errors
{
    using System;
    using System.Web;
    using Web.Asp.Provider;
    using Web.Asp.Provider.Cache;
    using Web.Asp.UI;

    public partial class Clear : VITPage
    {
        private string key
        {
            get
            {
                return this.Request[SettingsManager.Constants.SendClearData] ?? "";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.key.Length > 0)
            {
                Guid companyId = Guid.Empty;
                Guid.TryParse(key, out companyId);
                CacheProvider.ClearCache(companyId);
                HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + companyId] = null;
                var menukey = $"Menu-{companyId}-vi";
                GlobalCachingProvider.Instance.AddItem(menukey, null);
            }
            else
            {
                var menukey = $"Menu-{Config.Id}-vi";
                GlobalCachingProvider.Instance.AddItem(menukey, null);
                GlobalCachingProvider.Instance.AddItem(SettingsManager.Constants.LanguageCache + Config.Id, null);
                HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + Config.Id] = null;
            }
        }
    }
}