namespace Web.Asp.UrlRewrite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;

    using log4net;
    using Web.Business;
    using Web.Model;
    using Provider;

    public class RewriteUrl : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
        }

        protected static readonly ILog log = LogManager.GetLogger(typeof(RewriteUrl));

        public static string RawUrl
        {
            get { return HttpContext.Current.Request.RawUrl.Replace('\'', '/'); }
        }

        public static string PathUrl
        {
            get { return HttpContext.Current.Request.Path.ToLower(); }
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
        }

        private static void Context_BeginRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;
            var rawUrl = RawUrl.ToLower();

            if (!rawUrl.Contains("__browserlink") 
                && !rawUrl.Contains("/uploads/") 
                && !rawUrl.Contains("/includes/") 
                && !rawUrl.Contains("/templates/") 
                && !rawUrl.Contains("/modules/") 
                && !rawUrl.Contains("/error.aspx")
                && !rawUrl.Contains("/favicon.ico")
                && !rawUrl.Contains("-sitemap.xml")
                && !rawUrl.Contains("/jsonpost.aspx")
                && !rawUrl.Contains("/robots.txt")) 
            {
                var domain = HttpContext.Current.Request.Url.Authority;

                var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
                if (!domainCompanyMap.ContainsKey(domain) || domainCompanyMap[domain] == null || domainCompanyMap[domain].Id == Guid.Empty)
                {
                    var company = (new CompanyBLL()).GetCompanyByDomain(domain);
                    domainCompanyMap[domain] = company;
                    HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
                }

                if (PathUrl != "/" && PathUrl != "/home.aspx")
                {
                    var list = HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + domainCompanyMap[domain].Id] as List<SEOLinkModel>;
                    if (list == null || list.Count == 0 || !list.Any(l => l.SeoUrl.ToLower() == rawUrl))
                    {
                        list = (new SEOBLL()).GetAll(domainCompanyMap[domain].Id);
                        HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + domainCompanyMap[domain].Id] = list;
                    }

                    var query = string.Empty;
                    if (rawUrl.Contains("?"))
                    {
                        var urlarr = rawUrl.Split('?');
                        query = urlarr[urlarr.Length - 1];
                        rawUrl = urlarr[0];
                    }

                    var seo = list.FirstOrDefault(l => l.SeoUrl.ToLower() == rawUrl);
                    if (seo != null)
                    {
                        rawUrl = seo.Url;
                        if (!string.IsNullOrEmpty(seo.LanguageCode) && domainCompanyMap[domain].Language != seo.LanguageCode)
                        {
                            var querylang = SettingsManager.Constants.SendLanguage + "=" + seo.LanguageCode;
                            if (string.IsNullOrEmpty(query)) query = querylang;
                            else query = "&" + querylang;
                        }
                    }

                    // /articles/canh-sat/tag/canh sat
                    try
                    {
                        var newURL = new StringBuilder();
                        if (!rawUrl.Contains(".aspx"))
                        {
                            if (rawUrl.StartsWith("/")) rawUrl = rawUrl.Substring(1);
                            var arr = rawUrl.Split('/');
                            var component = arr[0];

                            //var acceptComponents = new List<string> {"home","aboutus","contact","article","articles","product","products",
                            //                            "media","medias","video","videos","event","events",
                            //                            "cart","cartinfo","search","tv","album","createwebsite","websites"
                            //                        };
                            //if (acceptComponents.Contains(component = arr[0]))
                            //{
                                newURL.Append($"/Templates/{domainCompanyMap[domain].Template}/Components/{component}.aspx");

                                if (arr.Length > 2)
                                {
                                    newURL.Append("?");

                                    var param = new StringBuilder();
                                    for (var i = 2; i < arr.Length; i++)
                                    {
                                        if (i > 2) param.Append("&");
                                        param.Append($"{arr[i++]}={arr[i]}");
                                    }
                                    newURL.Append(param.ToString());
                                }
                            //}
                        }
                        else
                        {
                            newURL.Append($"/Templates/{domainCompanyMap[domain].Template}/Components");
                            newURL.Append(rawUrl);
                        }

                        if (!string.IsNullOrEmpty(newURL.ToString()))
                        {
                            if (!string.IsNullOrEmpty(query))
                            {
                                if (newURL.ToString().Contains("?")) newURL.Append("&");
                                else newURL.Append("?");
                                newURL.Append(query);
                            }

                            httpApplication.Context.RewritePath(newURL.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        // xu ly loi
                        log.Error(ex);
                    }
                }
                else
                {
                    var url = string.Format($"/Templates/{domainCompanyMap[domain].Template}/Components/Home.aspx");
                    if (RawUrl.Contains("?")) url += "?" + RawUrl.Split('?')[1];
                    httpApplication.Context.RewritePath(url);
                }
            }
        }

        #endregion
    }
}
