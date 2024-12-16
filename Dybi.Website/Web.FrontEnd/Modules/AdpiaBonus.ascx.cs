using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.Provider.Cache;
using Web.Asp.UI;
using Web.Business;
using Web.Model;
using Library.Web.RSS;
using Library;
using Library.Web;
using HtmlAgilityPack;
using System.Text;

namespace Web.FrontEnd.Modules
{
    public partial class AdpiaBonus : VITModule
    {
        private const string LoginLink = "https://newpub.adpia.vn/login";
        
        protected string UserName { get; set; }
        protected string Password { get; set; }
        protected string API { get; set; }
        protected string Data { get; set; }
        protected string URL { get; set; }
        protected string HTMLSelector { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.UserName = this.GetValueParam<string>("UserName");
            this.Password = this.GetValueParam<string>("Password");
            this.API = this.GetValueParam<string>("API");
            this.URL = this.GetValueParam<string>("URL");
            this.HTMLSelector = this.GetValueParam<string>("HTMLSelector");

            var values = new System.Collections.Specialized.NameValueCollection
            {
                { "username", this.UserName },
                { "password",  this.Password },
            };

            CookieAwareWebClient webslient = new CookieAwareWebClient();
            try
            {
                webslient.Encoding = Encoding.UTF8;
                webslient.Login(LoginLink, values);
                
                if (!string.IsNullOrEmpty(URL))
                {
                    var htmlContent = webslient.DownloadString(URL);

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent); 

                    Data = doc.DocumentNode.SelectNodes("//" + HTMLSelector)
                                      .Select(p => p.OuterHtml)
                                      .FirstOrDefault();
                }
                else if (!string.IsNullOrEmpty(API))
                {
                    Data = webslient.DownloadString(API);
                }
            }
            catch (Exception ex)
            {
                Message.Error(ex.Message);
            }
        }
    }
}