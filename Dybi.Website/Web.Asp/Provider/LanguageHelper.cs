using System.Collections.Generic;
using System.Web;
using Library;
using Web.Asp.Provider.Cache;
using System;
using Web.Business;

namespace Web.Asp.Provider
{
    public class LanguageHelper
    {
        #region Singleton 
        private static LanguageHelper instance;

        public static LanguageHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LanguageHelper();
                }
                return instance;
            }
        }
        #endregion

        public string this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key)) return key;
                key = key.Trim();
                var url = HttpContext.Current.Request.RawUrl.Replace('\'', '/').ToLower();
                var webId = GlobalCachingProvider.Instance.GetItem<Guid>(URLProvider.Instance.Domain);
                if (webId == null || webId == Guid.Empty) return key;
                
                var langs = GlobalCachingProvider.Instance.GetItem<Dictionary<string, Dictionary<string, string>>>(SettingsManager.Constants.LanguageCache + webId);

                // neu mat cache thi get lai
                if (langs == null) langs = new Dictionary<string, Dictionary<string, string>>();
                if (!langs.ContainsKey(this.LanguageId) || langs[LanguageId] == null)
                {
                    langs[LanguageId] = (new CompanyBLL()).GetLanguageConfigs(webId, LanguageId);
                    
                    GlobalCachingProvider.Instance.AddItem(SettingsManager.Constants.LanguageCache + webId, langs);
                }

                return langs[LanguageId].GetValueOrDefault(key) ?? key;
            }
        }

        public string LanguageId
        {
            get
            {
                var language = HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] as string;
                if (!string.IsNullOrEmpty(language))
                {
                    return language;
                }
                else
                {
                    language = new Business.CompanyBLL().GetLanguage(URLProvider.Instance.Domain);
                    if (language != null)
                    {
                        HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] = language;
                        return language;
                    }
                    else return "vi";
                }
            }
            set
            {
                HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] = value;
            }
        }

        public List<string> Languages
        {
            get
            {
                var webId = GlobalCachingProvider.Instance.GetItem<Guid>(URLProvider.Instance.Domain);
                if (webId == null || webId == Guid.Empty) webId = (new Business.CompanyBLL().GetCompanyByDomain(URLProvider.Instance.Domain)).Id;

                var data = CacheProvider.GetCache<List<string>>(CacheProvider.Keys.Lan, webId);
                if (data == null || data.Count == 0)
                {
                    data = new Business.CompanyBLL().GetLanguage(webId);
                    CacheProvider.SetCache(data, CacheProvider.Keys.Lan, webId);
                }

                return data;
            }
        }
    }
}
