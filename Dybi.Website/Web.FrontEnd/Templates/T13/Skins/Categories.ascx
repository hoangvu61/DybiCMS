<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Categories.ascx.cs" Inherits="Web.FrontEnd.Modules.Categories" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>

<!-- gallery section start -->
<div class="category_section wow animate__ animate__fadeInUp animated" data-wow-duration="1.2s" data-wow-delay="0.2s" style="<%=string.IsNullOrEmpty(this.Skin.BodyBackground) ? "" : ";background-color:" + this.Skin.BodyBackground %>">
    <div class="container">
        <%if(Category.Id != Guid.Empty){ %>
        <div class="heading_container heading_center">
            <%if(HREF.CurrentComponent == "home"){ %>
            <h2>
                <a href="<%=HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), Category.Id, SettingsManager.Constants.SendCategory, Category.Id)%>" title="<%=Category.Title %>" style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
                    <%=Title %> 
                </a>
            </h2>
            <%} else {%>
            <h1 style="<%=string.IsNullOrEmpty(Skin.HeaderFontColor) ? "" : ";color:" + this.Skin.HeaderFontColor %><%=this.Skin.HeaderFontSize == 0 ? "" : ";font-size:" + this.Skin.HeaderFontSize + "px"%>">
                <%=Title %> 
            </h1>
            <p style="<%=string.IsNullOrEmpty(Skin.BodyFontColor) ? "" : ";color:" + this.Skin.BodyFontColor %><%=this.Skin.BodyFontSize == 0 ? "" : ";font-size:" + this.Skin.BodyFontSize + "px"%>">
                <%=Category.Brief %>
            </p>
            <%} %>
        </div>
        <%} %>
        <div class="subcat">
            <%{Data = Data.Where(e => e.Type == "PRO").ToList();} %>
			<%for (int i = 0; i < Data.Count; i++)
			{%>
                <%if(i > 0){ %> &nbsp;|&nbsp; <%} %>
                <a href="<%=HREF.LinkComponent(Category.ComponentList, Data[i].Title.ConvertToUnSign(), Data[i].Id, SettingsManager.Constants.SendCategory, Data[i].Id)%>">
                    <%=Data[i].Title %>
                </a>
			<%} %>
        </div>
    </div>
</div>
<!-- gallery section end -->

