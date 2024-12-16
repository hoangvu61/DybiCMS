namespace Web.FrontEnd
{
    using System;
    using System.Web;

    public partial class Error : Web.Asp.UI.VITPage
    {
        protected string Message { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Response.Status == "404") HttpContext.Current.Response.Redirect("/", true);
            Message = this.Server.UrlDecode(this.Request["msg"]);
            
            if (Message != null && Message.EndsWith("does not exist.")) HttpContext.Current.Response.Redirect("/", true);
        }
    }
}