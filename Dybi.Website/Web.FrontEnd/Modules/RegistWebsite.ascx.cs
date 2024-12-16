using Library;
using Library.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class RegistWebsite : VITModule
    {
        private TemplateBLL templateBLL;

        protected CreateWebModel Data { get; set; }
        protected string API { get; set; }
        protected string TemplateName { get; set; }
        protected TemplateModel Template { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            API = this.GetValueParam<string>("API");
            TemplateName = this.GetValueRequest<string>(SettingsManager.Constants.SendTemplate);

            this.templateBLL = new TemplateBLL();
            if (!Page.IsPostBack)
            {
                Data = new CreateWebModel();
                Data.Id = Guid.NewGuid();
                Session["RegistWebsite"] = Data;
            }
            else
            {
                Data = Session["RegistWebsite"] as CreateWebModel;
            }
            
            Template = templateBLL.GetAllTemplate(TemplateName);
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Data.FirstName = txtFirstName.Text;
            Data.LastName = txtLastName.Text;
            Data.UserName = txtUsertName.Text;
            Data.Password = txtPassword.Text;
            Data.Domain = txtDomain.Text;
            Data.WebsiteName = txtFullName.Text;
            Data.Phone = txtPhone.Text;
            Data.Email = txtEmail.Text;
            Data.TemplateName = TemplateName;

            try
            {
                //new ApiCaller().PostAsJsonAsync(API, JsonHelper.SerializeObject(Data));
                Message.Info(Language["success"]);
            }
            catch (Exception ex)
            {
                Message.Error(ex.Message);
            }
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            btnCreate.Text = Language["create"];
        }
    }
}