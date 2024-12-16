namespace Web.FrontEnd
{
    using System;
    using System.Web;
    using log4net;
    using System.Threading;
    using Library;
    using Web.Asp.Provider;
    using System.IO.Compression;
    using System.IO;

    //using System.Web;

    //using VIT.Provider;

    public class Global : System.Web.HttpApplication
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(Global));

        public Global()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.AddCombresRoute("Combres Route");
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //new MemberSecurityProvider().InitializePrincipal();
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is ThreadAbortException) return;

            Exception objErr = Server.GetLastError().GetBaseException();
            log.Error("Error Caught in Application_Error event");
            log.Error("Error in: " + Request.Url);
            log.Error("Error Message:" + objErr.Message);
            log.Error(objErr.TraceInformation());
            HttpContext.Current.Response.Redirect(new URLProvider().BaseUrl + "Error.aspx?msg=" + Server.UrlEncode(ex.Message));
        }

        void Session_Start(object sender, EventArgs e)
        {
            Session["CountError"] = 0;

            //Session.Timeout = 150;

            // check FreeDay
            if (SettingsManager.AppSettings.FreeDay < 1 || 30 < SettingsManager.AppSettings.FreeDay)
            {
                HttpContext.Current.Response.Redirect(
                    (new URLProvider()).BaseUrl + "Error.aspx?msg="
                    + Server.UrlEncode("Cấu hình không đúng: 0 < FreeDay < 31 "));
            }
        }

        void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            string acceptEncoding = app.Request.Headers["Accept-Encoding"];
            Stream prevUncompressedStream = app.Response.Filter;

            if (app.Context.CurrentHandler == null)
                return;

            if (!(app.Context.CurrentHandler is System.Web.UI.Page ||
                app.Context.CurrentHandler.GetType().Name == "SyncSessionlessHandler") ||
                app.Request["HTTP_X_MICROSOFTAJAX"] != null)
                return;

            if (acceptEncoding == null || acceptEncoding.Length == 0)
                return;

            acceptEncoding = acceptEncoding.ToLower();

            if (acceptEncoding.Contains("gzip"))
            {
                // gzip
                app.Response.Filter = new GZipStream(prevUncompressedStream,
                    CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (acceptEncoding.Contains("deflate") || acceptEncoding == "*")
            {
                // deflate
                app.Response.Filter = new DeflateStream(prevUncompressedStream,
                    CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }
}
